using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hapikit;
using Hapikit.Links;
using Hapikit.ResponseHandlers;
using haveibeenpwnd.net.hapisdk.Links;
using haveibeenpwnd.net.hapisdk.Messages;
using Newtonsoft.Json.Linq;

namespace haveibeenpwnd.net.hapisdk
{
    public class HibpService
    {
        private readonly HttpClient _httpClient;
        

        public HibpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("hapikit-sdk-tests", "1.0"));

        }

        public async Task<IEnumerable<BreachMessage>> GetBreachedAccountAsync(string account, string domain = null)
        {
            var link = new BreachedAccountLink()
            {
                Account = account,
                Domain = domain
            };
            IEnumerable<BreachMessage> bam = null;

            var machine = HibpMachineFactory.CreateMachine();
            machine.AddResponseAction(async (m, l, r) => { /* NOOP */ }, System.Net.HttpStatusCode.NotFound);
            machine.AddResponseAction(async (m, l, r) => { throw new ArgumentException(); }, System.Net.HttpStatusCode.BadRequest);
            machine.AddResponseAction<IEnumerable<BreachMessage>>((l, ba) => { bam = ba; }, System.Net.HttpStatusCode.OK);
            
            await _httpClient.FollowLinkAsync(link,machine);

            return bam;

        }

        public async Task<IEnumerable<PasteMessage>> GetPasteAccountAsync(string account, string domain = null)
        {
            var link = new PasteAccountLink()
            {
                Account = account

            };
            IEnumerable<PasteMessage> value = null;

            var machine = HibpMachineFactory.CreateMachine();
            machine.AddResponseAction<IEnumerable<PasteMessage>>((l, ba) => { value = ba; }, System.Net.HttpStatusCode.OK);

            await _httpClient.FollowLinkAsync(link, machine);

            return value;

        }

        public async Task<IEnumerable<BreachMessage>> GetBreachesAsync()
        {
            var link = new BreachesLink();
            List<BreachMessage> bam = null;

            var machine = HibpMachineFactory.CreateMachine();
            machine.AddResponseAction<List<BreachMessage>>((l, ba) => { bam = ba; }, System.Net.HttpStatusCode.OK);

            await _httpClient.FollowLinkAsync(link, machine);

            return bam;

        }
        public async Task<IEnumerable<string>> GetDataClassesAsync()
        {
            var link = new DataClassesLink();
            List<string> dataclasses = null;

            var machine = HibpMachineFactory.CreateMachine();
            machine.AddResponseAction(async (l, r) =>
            {
                var jDataClasses = JArray.Parse(await r.Content.ReadAsStringAsync());
                dataclasses = jDataClasses.Select(jDataClass => (string)jDataClass).ToList();
            }, System.Net.HttpStatusCode.OK, LinkHelper.GetLinkRelationTypeName<DataClassesLink>());

            await _httpClient.FollowLinkAsync(link, machine);

            return dataclasses;

        }

        public async Task<BreachMessage> GetBreachAsync(string name)
        {
            var link = new BreachLink()
            {
                Name = name
            };
            BreachMessage breach = null;

            var machine = HibpMachineFactory.CreateMachine();
            machine.AddResponseAction<BreachMessage>((l, ba) => { breach = ba; }, System.Net.HttpStatusCode.OK);

            await _httpClient.FollowLinkAsync(link, machine);

            return breach;

        }
    }
}
