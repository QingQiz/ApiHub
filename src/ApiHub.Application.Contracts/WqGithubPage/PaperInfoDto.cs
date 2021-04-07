using System.Collections.Generic;

namespace ApiHub.WqGithubPage
{
    public class PaperInfoDto
    {
        public IEnumerable<string> Author { get; set; }
        public string PaperTitle { get; set; }
        public string PaperPublisher { get; set; }
        public string OtherInfo { get; set; }
    }
}