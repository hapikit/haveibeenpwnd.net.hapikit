using System.IO;
using haveibeenpwnd.net.hapisdk;
using Newtonsoft.Json.Linq;
using Xunit;

namespace haveibeenpwnd.unit.tests
{
    public class ResponseHandingTests
    {


        [Fact]
        public void Empty_HibpDocument_should_return_empty_message()
        {

            var doc = new HibpDocument();

            var message = BreachMessage.Parse(doc);

            Assert.NotNull(message);

        }

        [Fact]
        public void Response_from_BreachLink_should_return_breach_Message()
        {

            var doc = new HibpDocument();
            doc.Properties.Add(HibpVocab.Name, new JProperty(HibpVocab.Name, "Bob"));
            doc.Properties.Add(HibpVocab.Title, new JProperty(HibpVocab.Title, "A big leak"));
            doc.Properties.Add(HibpVocab.Domain,new JProperty(HibpVocab.Domain, "foo.com"));

            var message = BreachMessage.Parse(doc);

            Assert.Equal("Bob",message.Name);
            Assert.Equal("A big leak", message.Title);
            Assert.Equal("foo.com", message.Domain);

        }
    }
}
