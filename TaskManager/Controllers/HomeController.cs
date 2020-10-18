using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.ApiHelpers;
using TaskManager.Models;
using TaskManager.Models.Responses;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            //var store = await new ApiClient().Get<StoreResponse>(ApiPath.Get(ApiControllerName.Store, 1));

            var store = new
            {
                Name = "RevCascade"
            };

            await new ApiClient().Post<object>(ApiPath.Create(ApiControllerName.Store), store);

            var stores = await new ApiClient().Get<IEnumerable<StoreResponse>>(ApiPath.Get(ApiControllerName.Store));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}