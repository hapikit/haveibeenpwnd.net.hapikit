using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hapikit.Links;
using Hapikit.ResponseHandlers;
using haveibeenpwnd.net.hapisdk.Links;
using haveibeenpwnd.net.hapisdk.Messages;

namespace haveibeenpwnd.net.hapisdk
{
    public static class HibpMachineFactory
    {
        public static HttpResponseMachine<T> CreateMachine<T>(T model)
        {
            var parserStore = CreateParserStore();

            var machine = new HttpResponseMachine<T>(model, parserStore);

            return machine;
        }
        public static HttpResponseMachine CreateMachine()
        {
            var parserStore = CreateParserStore();

            var machine = new HttpResponseMachine(parserStore);

            return machine;
        }

        private static ParserStore CreateParserStore()
        {
            var parserStore = new ParserStore();

            parserStore.AddMediaTypeParser("application/json", async (c) => HibpDocument.Parse(await c.ReadAsStreamAsync()));

            parserStore.AddLinkRelationParser<HibpDocument, BreachMessage>(
                LinkHelper.GetLinkRelationTypeName<BreachLink>(),
                BreachMessage.Parse);

            parserStore.AddLinkRelationParser<HibpDocument, IEnumerable<BreachMessage>>(
                LinkHelper.GetLinkRelationTypeName<BreachedAccountLink>(),
                (doc) => doc.ChildDocuments.Select(BreachMessage.Parse).ToList());

            parserStore.AddLinkRelationParser<HibpDocument, IEnumerable<PasteMessage>>(
                LinkHelper.GetLinkRelationTypeName<PasteAccountLink>(),
                (doc) => doc.ChildDocuments.Select(PasteMessage.Parse).ToList());

            parserStore.AddLinkRelationParser<HibpDocument, List<BreachMessage>>(
                LinkHelper.GetLinkRelationTypeName<BreachesLink>(),
                (doc) => doc.ChildDocuments.Select(BreachMessage.Parse).ToList());
            return parserStore;
        }
    }
}