using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
{
    public class StatusController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public StatusController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _apiClientService.Get<IEnumerable<StatusViewModel>>(ApiPath.Get(ApiControllerName.Status))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StatusViewModel> statuses)
        {
            if (ModelState.IsValid)
            {
                await _apiClientService.Post<object>(
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
                await _apiClientService.Put<object>(
                    ApiPath.BatchUpdate(ApiControllerName.Status), 
                    statuses
                );
            }

            return Json(await statuses.ToDataSourceResultAsync(request, ModelState));
        }
    }
}