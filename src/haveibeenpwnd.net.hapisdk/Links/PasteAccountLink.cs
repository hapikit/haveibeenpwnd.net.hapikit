using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hapikit.Links;
using Hapikit.Templates;

namespace haveibeenpwnd.net.hapisdk.Links
{

    [LinkRelationType("https://haveibeenpwnd.com/rels/pasteaccount")]
    public class PasteAccountLink : Link
    {
       
        public string Account { get; set; }

        public PasteAccountLink()
        {
            Template = new UriTemplate("https://haveibeenpwned.com/api/v2/pasteaccount/{account}");
        }
    }
}
