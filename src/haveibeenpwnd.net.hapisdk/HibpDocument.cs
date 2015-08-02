using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hapikit.Links;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace haveibeenpwnd.net.hapisdk
{
    public class HibpDocument
    {
        public Dictionary<string, JProperty> Properties { get; set; }

        public HibpDocument()
        {
            Properties = new Dictionary<string, JProperty>();
        }


        public static HibpDocument Parse(Stream stream)
        {
            var token = JToken.Load(new JsonTextReader(new StreamReader(stream)));
            return Parse(token);
        }

        public static HibpDocument Parse(JToken token)
        {
            var jroot = token as JObject;

            if (jroot == null) throw new Exception("Hibp documents must have an object as the root");

            var hibpDocument = new HibpDocument();

            foreach (var prop in jroot.Properties())
            {
               hibpDocument.Properties.Add(prop.Name, prop);
            }
            return hibpDocument;
        }


        public T ParseMessage<T>(Dictionary<string, Action<JProperty, T>> propertyMap, bool strict = false) where T : class, new()
        {

            var message = new T();
            foreach (var prop in Properties.Values)
            {
                if (propertyMap.ContainsKey(prop.Name))
                {
                    propertyMap[prop.Name](prop, message);
                }
                else
                {
                    if (strict) throw new Exception(String.Format("Unrecognized property {0}", prop.Name));
                }
            }

            return message;
        }


        public static string ReadAsString(JProperty prop)
        {
            return (string)prop.Value;
        }

        internal static bool ReadAsBoolean(JProperty prop)
        {
            return (bool)prop.Value;
        }

        internal static DateTime ReadAsDateTime(JProperty prop)
        {
            if (prop.Value == null) return DateTime.MinValue;

            return (DateTime)prop.Value;
        }

        internal static Guid ReadAsGuid(JProperty prop)
        {
            return (Guid)prop.Value;
        }

        internal static int ReadAsInteger(JProperty prop)
        {
            return (int)prop.Value;
        }

        internal static T ReadAsLink<T>(JObject linkObject) where T : Link, new()
        {
            var href = linkObject.Property("href");
            return new T()
            {
                Target = new Uri((string)href.Value, UriKind.RelativeOrAbsolute),
                Template = null
            };
        }




        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static ILinkFactory CreateLinkFactory()
        {
            var linkFactory = new LinkFactory();


            return linkFactory;
        }
    }
}
