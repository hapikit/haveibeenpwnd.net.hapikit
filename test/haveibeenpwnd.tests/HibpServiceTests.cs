using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using haveibeenpwnd.net.hapisdk;
using Xunit;

namespace haveibeenpwnd.tests
{
    public class HibpServiceTests
    {
        [Fact]
        public async Task TestBreachedAccount()
        {
            var httpClient = new HttpClient();
            var service = new HibpService(httpClient);

            var breachedAccounts = await service.GetBreachedAccountAsync("bob@gmail.com");

            Assert.NotNull(breachedAccounts);
            Assert.True(breachedAccounts.Count() > 0);
        }

        [Fact]
        public async Task TestPasteAccount()
        {
            var httpClient = new HttpClient();
            var service = new HibpService(httpClient);

            var pasteAccount = await service.GetPasteAccountAsync("bob@gmail.com");

            Assert.NotNull(pasteAccount);
            Assert.True(pasteAccount.Count() > 0);
        }

        [Fact]
        public async Task TestBreaches()
        {
            var httpClient = new HttpClient();
            var service = new HibpService(httpClient);

            var breaches = await service.GetBreachesAsync();

            Assert.NotNull(breaches);
            Assert.True(breaches.Count() > 0);
        }

        [Fact]
        public async Task GetBreach()
        {
            var httpClient = new HttpClient();
            var service = new HibpService(httpClient);

            var breach = await service.GetBreachAsync("adobe");

            Assert.NotNull(breach);
        }

        [Fact]
        public async Task GetDataClasses()
        {
            var httpClient = new HttpClient();
            var service = new HibpService(httpClient);

            var dataclasses = await service.GetDataClassesAsync();

            Assert.NotNull(dataclasses);
            Assert.True(dataclasses.Count() > 0);
        }
    }
}
