using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;

namespace WuhanJamesHubApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JJBController: BaseController
    {
        private readonly IMemoryCache _cache;
        private readonly Random _random = new Random();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly JJBService jjbService;

        public JJBController(IMemoryCache memoryCache, JJBService jjbService)
        {
            _cache = memoryCache;
            this.jjbService = jjbService;
        }

        [HttpPost]
        public IActionResult CheckData(List<List<string>> request)
        {
            foreach (var item in request)
            {
                // 判断长度是否为 52, 57, 62, 67，并且最后一个元素是否为空字符串
                if ((item.Count == 52 || item.Count == 57 || item.Count == 62 || item.Count == 67)
                    && string.IsNullOrEmpty(item.Last()))
                {
                    // 移除最后一个元素
                    item.RemoveAt(item.Count - 1);
                }
            }

            var list = new List<CheckDataModel>();
            var memberList = jjbService.LoadMemberList(request, out string loadErrMsg);
            if (memberList == null)
            {
                return Ok(new { code = 50001, errMsg = loadErrMsg });
            }
            foreach (var member in memberList)
            {
                var checkList = member.Check();
                var builder = new StringBuilder();
                if (checkList.Count > 0)
                {
                    list.Add(new CheckDataModel()
                    {
                         Title = member.ListName,
                        CheckList = checkList
                    });
                }
            }
            return new CustomActionResult(list);
        }
    }
    public class CheckDataModel
    {
        public string Title { get; set; }
        public List<string> CheckList { get; set; }
    }
}