using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using haveibeenpwnd.net.hapisdk;
using Newtonsoft.Json.Linq;
using Xunit;

namespace haveibeenpwnd.unit.tests
{
    public class HibpDocumentTests
    {

        [Fact]
        public void Empty_stream_should_produce_empty_HibpDocument()
        {
            var sourceStream = new MemoryStream();

            var doc = HibpDocument.Parse(sourceStream);

            Assert.NotNull(doc);
        }

        [Fact]
        public void Extract_properties_from_json_HibpDocument()
        {
            var sourceStream = CreateJsonStream("{ \"foo\" : \"bar\", \"baz\" : \"boo\"}");
            var doc = HibpDocument.Parse(sourceStream);

            Assert.Equal(2, doc.Properties.Count);
            Assert.Equal("foo", doc.Properties.First().Key);
            Assert.Equal("bar", HibpDocument.ReadAsString(doc.Properties["foo"]));
            Assert.Equal("boo", HibpDocument.ReadAsString(doc.Properties["baz"]));
        }

        [Fact]
        public void Hibp_document_does_not_support_array_at_root()
        {
            bool exceptionThrown = false;
            var sourceStream = CreateJsonStream("[]");
            try
            {
                var doc = HibpDocument.Parse(sourceStream);
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);
            
        }

        [Fact]
        public void Hibp_document_does_not_support_string_at_root()
        {
            bool exceptionThrown = false;
            var sourceStream = CreateJsonStream("\"hello\"");
            try
            {
                var doc = HibpDocument.Parse(sourceStream);
            }
            catch (ArgumentException ex)
            {
                exceptionThrown = true;
            }
            Assert.True(exceptionThrown);

        }

        [Fact]
        public void HibpDocument_can_map_properties_to_types()
        {
            var sourceStream = CreateJsonStream("{ \"name\" : \"Bob\", \"Age\" : 24, \"DOB\" : \"19710720\" }");
            var doc = HibpDocument.Parse(sourceStream);
            var parseMap =  new HibpDocument.HibpMap<Person>
            {
                {"name", (p, t) => t.Name = HibpDocument.ReadAsString(p)},
                {"Age", (p, t) => t.Age = HibpDocument.ReadAsInteger(p)}
            };

            var person = doc.ParseMessage<Person>(parseMap);

            Assert.Equal("Bob", person.Name);
            Assert.Equal(24, person.Age);
        }


        private static MemoryStream CreateJsonStream(string jsontext)
        {
            var sourceStream = new MemoryStream();
            var sw = new StreamWriter(sourceStream);
            sw.Write(jsontext);
            sw.Flush();
            sourceStream.Position = 0;
            return sourceStream;
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
    }
}
