using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.ApiHelpers;
using TaskManagerApp.Models.ViewModels;

namespace TaskManagerApp.Controllers
{
    public class AssignmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await ApiClient
                .Get<IEnumerable<AssignmentViewModel>>(ApiPath.Get(ApiControllerName.Assignment))
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AssignmentRequest request)
        {
            if (request.AssignmentId > 0)
            {
                await ApiClient
                    .Put<AssignmentRequest>(
                        ApiPath.Update(ApiControllerName.Assignment), 
                        request
                    )
                    .ConfigureAwait(false);

                return RedirectToAction(nameof(Index));
            }

            await ApiClient
                .Post<AssignmentRequest>(
                    ApiPath.Create(ApiControllerName.Assignment), 
                    request
                )
                .ConfigureAwait(false);

            return RedirectToAction(nameof(Index));
        }
    }
}