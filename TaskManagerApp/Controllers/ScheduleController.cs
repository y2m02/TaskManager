using System;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerApi.Models.Responses;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.Enums;
using TaskManagerApp.Models.Requests;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IApiClientService _apiClientService;

        public ScheduleController(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public async Task<IActionResult> Index()
        {
            var itemTypes = await _apiClientService
                .Post<List<ItemTypeResponse>>(
                ApiPath.Get(ApiControllerName.ItemType),
                new List<ItemType> { ItemType.Status }
            )
            .ConfigureAwait(false);

            ViewBag.Assignments = new List<ItemTypeResponse>();
            ViewBag.Statuses = itemTypes.Where(w => w.Type == ItemType.Status);

            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _apiClientService
                .Get<IEnumerable<ScheduleViewModel>>(ApiPath.Get(ApiControllerName.Schedule))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<JsonResult> Upsert(ScheduleRequest request)
        {
            if (request.ScheduleId > 0)
            {
                await _apiClientService
                    .Put<ScheduleRequest>(
                        ApiPath.Update(ApiControllerName.Schedule),
                        request
                    )
                    .ConfigureAwait(false);

                return Json("updated");
            }

            await _apiClientService
                .Post<ScheduleRequest>(
                    ApiPath.Create(ApiControllerName.Schedule),
                    request
                )
                .ConfigureAwait(false);

            return Json("created");
        }
    }
}