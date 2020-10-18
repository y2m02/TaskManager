using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected readonly IMapper Mapper;

        public BaseApiController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}