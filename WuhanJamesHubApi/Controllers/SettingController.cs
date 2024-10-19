using Microsoft.AspNetCore.Mvc;

namespace WuhanJamesHubApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingController : BaseController
    {
        private readonly IWebHostEnvironment _env;

        public SettingController(IWebHostEnvironment env)
        {
            _env = env;
        }
    }
}