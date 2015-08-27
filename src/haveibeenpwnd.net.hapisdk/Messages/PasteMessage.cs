using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace haveibeenpwnd.net.hapisdk.Messages
{
    public class PasteMessage
    {
        public string Source { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int EmailCount { get; set; }


        private static readonly HibpDocument.HibpMap<PasteMessage> _PropertyMap = new HibpDocument.HibpMap<PasteMessage>();

        static PasteMessage()
        {
            _PropertyMap.Add(HibpVocab.Source,       (p,t) => t.Source = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.Id,       (p,t) => t.Id = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.Title,        (p,t) => t.Title = HibpDocument.ReadAsString(p));
            _PropertyMap.Add(HibpVocab.Date,        (p,t) => t.Date = HibpDocument.ReadAsDateTime(p));
            _PropertyMap.Add(HibpVocab.EmailCount,        (p,t) => t.EmailCount= HibpDocument.ReadAsInteger(p));

        }
        public static PasteMessage Parse(HibpDocument doc)
        {
            return doc.ParseMessage(_PropertyMap);
        }
    }
}
