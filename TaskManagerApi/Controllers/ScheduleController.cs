using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Requests.Schedules;
using TaskManagerApi.Models.Responses;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Controllers
{
    public class ScheduleController : BaseApiController
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleController(
            IMapper mapper,
            IScheduleRepository scheduleRepository
        ) : base(mapper)
        {
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ScheduleResponse>> GetAll()
        {
            return Mapper.Map<IEnumerable<ScheduleResponse>>(await _scheduleRepository.GetAll());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<ScheduleResponse> GetById(int id)
        {
            return Mapper.Map<ScheduleResponse>(await _scheduleRepository.GetById(id));
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(ScheduleRequest request)
        {
            await _scheduleRepository.Create(Mapper.Map<Schedule>(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update(UpdateScheduleRequest request)
        {
            await _scheduleRepository.Update(Mapper.Map<Schedule>(request));
        }
    }
}