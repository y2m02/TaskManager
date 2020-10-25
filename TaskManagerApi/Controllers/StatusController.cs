using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Requests.Statuses;
using TaskManagerApi.Models.Responses;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Controllers
{
    public class StatusController : BaseApiController
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(
            IMapper mapper,
            IStatusRepository statusRepository
        ) : base(mapper)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<StatusResponse>> GetAll()
        {
            return Mapper.Map<IEnumerable<StatusResponse>>(await _statusRepository.GetAll());
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<StatusResponse> GetById(int id)
        {
            return Mapper.Map<StatusResponse>(await _statusRepository.GetById(id));
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(StatusRequest request)
        {
            await _statusRepository.Create(Mapper.Map<Status>(request));
        }

        [HttpPost]
        [Route("BatchCreate")]
        public async Task BatchCreate(IEnumerable<StatusRequest> request)
        {
            await _statusRepository.BatchCreate(Mapper.Map<IEnumerable<Status>>(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update(UpdateStatusRequest request)
        {
            await _statusRepository.Update(Mapper.Map<Status>(request));
        }

        [HttpPut]
        [Route("BatchUpdate")]
        public async Task BatchUpdate(IEnumerable<UpdateStatusRequest> request)
        {
            await _statusRepository.BatchUpdate(Mapper.Map<IEnumerable<Status>>(request));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task Delete(int id)
        {
            await _statusRepository.Delete(Mapper.Map<Status>(new DeleteStatusRequest(id)));
        }
    }
}