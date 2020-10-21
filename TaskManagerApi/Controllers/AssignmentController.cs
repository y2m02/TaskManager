using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Requests.Assignments;
using TaskManagerApi.Models.Responses;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Controllers
{
    public class AssignmentController : BaseApiController
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(
            IMapper mapper,
            IAssignmentRepository assignmentRepository
        ) : base(mapper)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<AssignmentResponse>> GetAll()
        {
            return Mapper.Map<IEnumerable<AssignmentResponse>>(await _assignmentRepository.GetAll());
        }

        [HttpGet]
        [Route("GetAllForDropDownList/{id}")]
        public async Task<IEnumerable<ItemTypeResponse>> GetAllForDropDownList(int id)
        {
            var itemTypes = Mapper.Map<IEnumerable<ItemTypeResponse>>(
                await _assignmentRepository.GetAllForDropDownList(id)
            );

            return itemTypes;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<AssignmentResponse> GetById(int id)
        {
            return Mapper.Map<AssignmentResponse>(await _assignmentRepository.GetById(id));
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(AssignmentRequest request)
        {
            await _assignmentRepository.Create(Mapper.Map<Assignment>(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update(UpdateAssignmentRequest request)
        {
            await _assignmentRepository.Update(Mapper.Map<Assignment>(request));
        }
    }
}