using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models.Enums;
using TaskManagerApi.Models.Responses;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Controllers
{
    public class ItemTypeController : BaseApiController
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IStatusRepository _statusRepository;

        public ItemTypeController(
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

        [HttpPost]
        [Route("Get")]
        public async Task<IEnumerable<ItemTypeResponse>> Get(List<ItemType> types)
        {
            var itemTypes = new List<ItemTypeResponse>();

            if (types.Contains(ItemType.Store))
            {
                itemTypes.AddRange(Mapper.Map<IEnumerable<ItemTypeResponse>>(await _storeRepository.GetAll()));
            }

            if (types.Contains(ItemType.Assignment))
            {
                itemTypes.AddRange(Mapper.Map<IEnumerable<ItemTypeResponse>>(await _assignmentRepository.GetAllForDropDownList()));
            }

            if (types.Contains(ItemType.Status))
            {
                itemTypes.AddRange(Mapper.Map<IEnumerable<ItemTypeResponse>>(await _statusRepository.GetAll()));
            }

            return itemTypes;
        }
    }
}
