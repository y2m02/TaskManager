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
        private readonly IApiClientService _apiClientService;

        public StoreController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _apiClientService.Get<IEnumerable<StoreViewModel>>(ApiPath.Get(ApiControllerName.Store))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            if (ModelState.IsValid)
            {
                await _apiClientService.Post<object>(
                    ApiPath.BatchCreate(ApiControllerName.Store), 
                    stores
                );
            }

            return Json(await stores.ToDataSourceResultAsync(request, ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<StoreViewModel> stores)
        {
            if (ModelState.IsValid)
            {
                await _apiClientService.Put<object>(
                    ApiPath.BatchUpdate(ApiControllerName.Store), 
                    stores
                );
            }

            return Json(await stores.ToDataSourceResultAsync(request, ModelState));
        }
    }
}