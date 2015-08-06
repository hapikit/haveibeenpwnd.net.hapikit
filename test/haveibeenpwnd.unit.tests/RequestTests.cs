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
    public class RequestTests
    {

        [Fact]
        public void CreateGetBreachesRequest()
        {
            var breachesLink = new BreachesLink();  // Uses Accept header versioning

            HttpRequestMessage request = breachesLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/breaches", request.RequestUri.AbsoluteUri);
            Assert.Contains("application/vnd.haveibeenpwned.v2+json", request.Headers.Accept.ToString());
            
        }

        [Fact]
        public void CreateGetBreachRequest()
        {
            var breachLink = new BreachLink()   // Uses path segment versioning
            {
                Name = "foo"
            };

            HttpRequestMessage request = breachLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/v2/breach/foo", request.RequestUri.AbsoluteUri);
        }

        [Fact]
        public void CreateGetBreachedAccount()
        {
            var breachLink = new BreachedAccountLink()   // Uses path segment versioning
            {
                Account = "bar@foo.com"
            };

            HttpRequestMessage request = breachLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/v2/breachedaccount/bar%40foo.com", request.RequestUri.AbsoluteUri);
        }

        [Fact]
        public void CreateGetBreachedAccountTruncated()
        {
            var breachLink = new BreachedAccountLink()   // Uses path segment versioning
            {
                Account = "bar@foo.com",
                TruncateResponse = true
            };

            HttpRequestMessage request = breachLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/v2/breachedaccount/bar%40foo.com?truncateResponse=True", request.RequestUri.AbsoluteUri);
        }

        [Fact]
        public void CreateGetBreachedAccountForADomain()
        {
            var breachLink = new BreachedAccountLink()   // Uses path segment versioning
            {
                Account = "bar@foo.com",
                Domain = "foobar.com"
            };

            HttpRequestMessage request = breachLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/v2/breachedaccount/bar%40foo.com?domain=foobar.com", request.RequestUri.AbsoluteUri);
        }

        [Fact]
        public void CreateGetBreachedAccountForADomainTruncated()
        {
            var breachLink = new BreachedAccountLink()   // Uses path segment versioning
            {
                Account = "bar@foo.com",
                Domain = "foobar.com",
                TruncateResponse = true
            };

            HttpRequestMessage request = breachLink.CreateRequest();

            Assert.Equal(HttpMethod.Get, request.Method);
            Assert.Equal("https://haveibeenpwned.com/api/v2/breachedaccount/bar%40foo.com?truncateResponse=True&domain=foobar.com", request.RequestUri.AbsoluteUri);
        }

    }
}
