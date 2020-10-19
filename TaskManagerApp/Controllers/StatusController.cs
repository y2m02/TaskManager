using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
{
    public class StatusController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await GetApiClient()
                .Get<IEnumerable<StatusViewModel>>(ApiPath.Get(ApiControllerName.Status))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StatusViewModel> statuses)
        {
            if (ModelState.IsValid)
            {
                await GetApiClient().Post<object>(
                    ApiPath.BatchCreate(ApiControllerName.Status), 
                    statuses
                );
            }

            return Json(await statuses.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StatusViewModel> statuses)
        {
            if (ModelState.IsValid)
            {
                await GetApiClient().Put<object>(
                    ApiPath.BatchUpdate(ApiControllerName.Status), 
                    statuses
                );
            }

            return Json(await statuses.ToDataSourceResultAsync(request, ModelState));
        }
    }
}