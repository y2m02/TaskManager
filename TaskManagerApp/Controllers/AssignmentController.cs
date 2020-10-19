using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Models.Responses;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.Enums;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public AssignmentController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Stores = await _apiClientService
                .Post<IEnumerable<ItemTypeResponse>>(
                ApiPath.Get(ApiControllerName.ItemType),
                new List<ItemType> { ItemType.Store }
            )
            .ConfigureAwait(false);

            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _apiClientService
                .Get<IEnumerable<AssignmentViewModel>>(ApiPath.Get(ApiControllerName.Assignment))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Upsert(AssignmentRequest request)
        {
            if (request.AssignmentId > 0)
            {
                await _apiClientService
                    .Put<AssignmentRequest>(
                        ApiPath.Update(ApiControllerName.Assignment),
                        request
                    )
                    .ConfigureAwait(false);

                return Json("");
            }

            await _apiClientService
                .Post<AssignmentRequest>(
                    ApiPath.Create(ApiControllerName.Assignment),
                    request
                )
                .ConfigureAwait(false);

              return Json("");
        }
    }
}