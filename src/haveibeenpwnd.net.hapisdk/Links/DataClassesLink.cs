using Hapikit.Links;
using Hapikit.Templates;

namespace haveibeenpwnd.net.hapisdk
{
    public class DataClassesLink : Link
    {
        public DataClassesLink()
        {
            Template = new UriTemplate("https://haveibeenpwned.com/api/v2/dataclasses");
        }
    }
}