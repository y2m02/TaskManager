using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
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