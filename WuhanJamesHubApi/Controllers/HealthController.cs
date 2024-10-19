using Microsoft.AspNetCore.Mvc;

namespace WuhanJamesHubApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "ok5";
        }
    }
}