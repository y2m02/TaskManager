using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TaskManager.ApiHelpers;
using TaskManager.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.Controllers
{
    public class StoreController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await new ApiClient()
                .Get<IEnumerable<StoreViewModel>>(ApiPath.Get(ApiControllerName.Store))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }
    }
}