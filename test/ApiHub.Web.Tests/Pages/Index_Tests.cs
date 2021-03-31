using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ApiHub.Pages
{
    public class Index_Tests : ApiHubWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
