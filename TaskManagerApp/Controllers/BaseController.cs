using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.ApiHelpers;

namespace TaskManagerApp.Controllers
{
    public class BaseController : Controller
    {
        protected ApiClient GetApiClient() => new ApiClient();
    }
}