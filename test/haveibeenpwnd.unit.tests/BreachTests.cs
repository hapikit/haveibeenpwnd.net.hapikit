using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using haveibeenpwnd.net.hapisdk;
using Xunit;

namespace haveibeenpwnd.unit.tests
{
    public class BreachTests
    {

        [Fact]
        public void CreateGetBreachesRequest()
        {
            var breachesLink = new BreachesLink();

            var request = breachesLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/breaches", request.RequestUri.AbsoluteUri);
            Assert.Contains("application/vnd.haveibeenpwned.v2+json", request.Headers.Accept.ToString());
            
        }
    }
}
