using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hapikit.ResponseHandlers;
using haveibeenpwnd.net.hapisdk;
using Hapikit;
using Xunit;

namespace haveibeenpwnd.tests
{
    public class ReadTests
    {
        private HttpClient _httpClient;

        public ReadTests()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("hapikit-sdk-tests", "1.0"));
        }

        [Fact]
        public async Task GetBreaches()
        {
            var breachesLink = new BreachesLink();

            var response = await _httpClient.FollowLinkAsync(breachesLink);

            Assert.True(response.IsSuccessStatusCode);

            var result = await response.Content.ReadAsStringAsync();

        }

        [Fact]
        public async Task TestMe()
        {
            var breachAccountLink = new BreachedAccountLink()
            {
                Account = "darrel@tavis.ca"
            };

            var isPwnd = new Model<bool>();

            var machine = AttachBreachedResponseMachine(isPwnd);

            await _httpClient.FollowLinkAsync(breachAccountLink, machine);


            Assert.False(isPwnd.Value);

        }

        private static HttpResponseMachine<Model<bool>> AttachBreachedResponseMachine(Model<bool> isPwnd)
        {
            var machine = new HttpResponseMachine<Model<bool>>(isPwnd);
            machine.AddResponseHandler(async (m, l, r) =>
            {
                m.Value = false;
                return r;
            }, HttpStatusCode.NotFound);

            machine.AddResponseHandler(async (m, l, r) =>
            {
                m.Value = true;
                return r;
            }, HttpStatusCode.OK);
            return machine;
        }

        [Fact]
        public async Task TestBob()
        {
            var breachAccountLink = new BreachedAccountLink()
            {
                Account = "bob@gmail.com",
                Domain = "adobe.com",
                TruncateResponse = true
            };


            var isPwnd = new Model<bool>();

            var machine = AttachBreachedResponseMachine(isPwnd);

            var response = await _httpClient.FollowLinkAsync(breachAccountLink, machine);

            Assert.True(isPwnd.Value);

            var result = await response.Content.ReadAsStringAsync();

        }

        [Fact]
        public async Task GetDataClasses()
        {
            var dataClassLink = new DataClassLink();

            var response = await _httpClient.FollowLinkAsync(dataClassLink);

            Assert.True(response.IsSuccessStatusCode);

            var result = await response.Content.ReadAsStringAsync();

        }
        [Fact]
        public async Task GetBreach()
        {
            var breachLink = new BreachLink()
            {
                Name = "adobe"
            };


            var response = await _httpClient.FollowLinkAsync(breachLink);

            var doc = HibpDocument.Parse(await response.Content.ReadAsStreamAsync());
            var message = BreachMessage.Parse(doc);

            Assert.NotNull(message);
            Assert.Equal("Adobe", message.Name);

            

        }
    }


}
