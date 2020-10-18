using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAll")]
        public async Task<IEnumerable<AssignmentResponse>> GetAll()
        {
            return Mapper.Map<IEnumerable<AssignmentResponse>>(await _assignmentRepository.GetAll());
        }

        [HttpGet]
        [Route("GetAllForDropDownList")]
        public async Task<IEnumerable<AssignmentForDropDownListResponse>> GetAllForDropDownList()
        {
            return Mapper.Map<IEnumerable<AssignmentForDropDownListResponse>>(await _assignmentRepository.GetAllForDropDownList());
        }

        [HttpGet]
        [Route("GetById/{id}")]
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