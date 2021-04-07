using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using ApiHub.WqGithubPage;
using Volo.Abp.Identity;
using Xunit;

namespace ApiHub.Samples
{
    /* This is just an example test class.
     * Normally, you don't test code of the modules you are using
     * (like IIdentityUserAppService here).
     * Only test your own application services.
     */
    public class SampleAppServiceTests : ApiHubApplicationTestBase
    {
        private readonly IIdentityUserAppService _userAppService;
        private readonly WqGithubPageAppService _wqGithubPageAppService;

        public SampleAppServiceTests()
        {
            _userAppService = GetRequiredService<IIdentityUserAppService>();
            _wqGithubPageAppService = GetRequiredService<WqGithubPageAppService>();
        }

        [Fact]
        public async Task Initial_Data_Should_Contain_Admin_User()
        {
            //Act
            var result = await _userAppService.GetListAsync(new GetIdentityUsersInput());

            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(u => u.UserName == "admin");
        }

        [Fact]
        public void Paper_Info_Should_Be_Converted_Correctly()
        {
            var paperInfo = new []
            {
                "M. Jiang, Y. Yuan, and Q. Wang*, “Asymmetric Cross-View Dictionary Learning for Person Re-identification,” Proc. International Conference on Acoustic, Speech and Signal Processing (ICASSP), pp. 1228-1232, 2017. Oral [PDF]",
                "J. Zhang, Q. Wang*, and Y. Yuan, “Metric Learning by Simultaneously Learning Linear Transformation Matrix and Weight Matrix for Person Re-identification,” IET Computer Vision (IET CV), vol. 13, no. 4, pp. 428-434, 2019. [PDF]"
            };
            
            var paperInfoConverted = paperInfo.Select(WqGithubPageAppService.ConvertPaperString).ToList();

            paperInfoConverted[0].Author.Aggregate("", (s, s1) => $"{s},{s1}").ShouldBe(",M. Jiang,Y. Yuan,Q. Wang*");
            paperInfoConverted[0].PaperTitle.ShouldBe("Asymmetric Cross-View Dictionary Learning for Person Re-identification");
            paperInfoConverted[0].PaperPublisher.ShouldBe("Proc. International Conference on Acoustic, Speech and Signal Processing (ICASSP)");
            paperInfoConverted[0].OtherInfo.ShouldBe("pp. 1228-1232, 2017. Oral");

            paperInfoConverted[1].Author.Aggregate("", (s, s1) => $"{s},{s1}").ShouldBe(",J. Zhang,Q. Wang*,Y. Yuan");
            paperInfoConverted[1].PaperTitle.ShouldBe("Metric Learning by Simultaneously Learning Linear Transformation Matrix and Weight Matrix for Person Re-identification");
            paperInfoConverted[1].PaperPublisher.ShouldBe("IET Computer Vision (IET CV)");
            paperInfoConverted[1].OtherInfo.ShouldBe("vol. 13, no. 4, pp. 428-434, 2019");
        }
    }
}
