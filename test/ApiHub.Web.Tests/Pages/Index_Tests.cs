using System;
using System.Net;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ApiHub.Pages
{
    public class Index_Tests : ApiHubWebTestBase
    {
        [Fact]
        public async Task Index()
        {
            var response = await GetResponseAsStringAsync("/", HttpStatusCode.Redirect);
            response.ShouldBeNullOrEmpty();
        }
    }
}
