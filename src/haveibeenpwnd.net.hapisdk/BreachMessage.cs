using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hapikit.Links;
using Newtonsoft.Json.Linq;

namespace haveibeenpwnd.net.hapisdk
{
    public class BreachMessage
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public DateTime BreachDate { get; set; }
        public DateTime AddedDate { get; set; }
        public int PwnCount { get; set; }
        public string[] DataClasses { get; set; }


        private static readonly Dictionary<string, Action<JProperty, BreachMessage>> _PropertyMap = new Dictionary<string, Action<JProperty, BreachMessage>>();

        static BreachMessage()
        {
            _PropertyMap.Add("Name",       (p,t) => t.Name = HibpDocument.ReadAsString(p));
            _PropertyMap.Add("Title",        (p,t) => t.Title = HibpDocument.ReadAsString(p));
            _PropertyMap.Add("Domain",        (p,t) => t.Domain = HibpDocument.ReadAsString(p));
            _PropertyMap.Add("BreachDate",        (p,t) => t.BreachDate = HibpDocument.ReadAsDateTime(p));
            _PropertyMap.Add("AddedDate",        (p,t) => t.AddedDate = HibpDocument.ReadAsDateTime(p));
            _PropertyMap.Add("Description",        (p,t) => t.Description = HibpDocument.ReadAsString(p));

        }
        public static BreachMessage Parse(HibpDocument doc)
        {
            return doc.ParseMessage(_PropertyMap);
        }
    }
}


