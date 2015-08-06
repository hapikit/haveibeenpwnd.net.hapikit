using System;

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


        private static readonly HibpDocument.HibpMap<BreachMessage> _PropertyMap = new HibpDocument.HibpMap<BreachMessage>();

        static BreachMessage()
        {
            _PropertyMap.Add(HibpVocab.Name,       (p,t) => t.Name = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.Title,        (p,t) => t.Title = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.Domain,        (p,t) => t.Domain = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.BreachDate,        (p,t) => t.BreachDate = HibpDocument.ReadAsDateTime(p));
            _PropertyMap.Add(HibpVocab.AddedDate,        (p,t) => t.AddedDate = HibpDocument.ReadAsDateTime(p));
            _PropertyMap.Add(HibpVocab.Description,        (p,t) => t.Description = HibpDocument.ReadAsString(p));

        }
        public static BreachMessage Parse(HibpDocument doc)
        {
            return doc.ParseMessage(_PropertyMap);
        }
    }
}


