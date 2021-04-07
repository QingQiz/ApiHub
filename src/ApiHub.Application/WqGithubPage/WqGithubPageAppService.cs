using System.Linq;
using System.Text.RegularExpressions;
using ApiHub.ApplicationAttribute;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;

namespace ApiHub.WqGithubPage
{
    [Authorize]
    public class WqGithubPageAppService : ApiHubAppService
    {
        [RemoteService(false)]
        public static PaperInfoDto ConvertPaperString(string content)
        {
            var reg = new Regex(@"(.*?)\s*“(.*?)”\s*").Match(content);
            var res = new PaperInfoDto
            {
                Author = reg.Groups[1].Value.Trim(',', ' ')
                    .Split(',')
                    .Select(a => a.Replace("and", "").Trim(' ')),
                PaperTitle = reg.Groups[2].Value.Trim(',', ' '),
            };
            content = content[reg.Groups[0].Length..];

            reg = new Regex(@"(.*?),\s*(pp|vol|\d)([^[]*)[.|[]?", RegexOptions.IgnoreCase).Match(content);

            res.PaperPublisher = reg.Groups[1].Value.Trim(',', ' ');
            res.OtherInfo = (reg.Groups[2].Value + reg.Groups[3].Value).Trim('.', ' ');
            
            return res;
        }
    }
}