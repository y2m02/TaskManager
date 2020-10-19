using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : BaseApiController
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStatusRepository _statusRepository;

        public ItemController(
            IMapper mapper, 
            IStoreRepository storeRepository, 
            IAssignmentRepository assignmentRepository, 
            IStatusRepository statusRepository
        ) : base(mapper)
        {
            _storeRepository = storeRepository;
            _assignmentRepository = assignmentRepository;
            _statusRepository = statusRepository;
        }
    }
}
