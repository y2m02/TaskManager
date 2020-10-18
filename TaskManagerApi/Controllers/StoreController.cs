using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Requests.Stores;
using TaskManagerApi.Models.Responses;
using TaskManagerApi.Repositories;

namespace TaskManagerApi.Controllers
{
    public class StoreController : BaseApiController
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(
            IStoreRepository storeRepository,
            IMapper mapper
        ) : base(mapper)
        {
            _storeRepository = storeRepository;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<StoreResponse>> GetAll()
        {
            var stores = Mapper.Map<IEnumerable<StoreResponse>>(await _storeRepository.GetAll());

            return stores;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<StoreResponse> GetById(int id)
        {
            var store = Mapper.Map<StoreResponse>(await _storeRepository.GetById(id));

            return store;
        }

        [HttpPost]
        [Route("Create")]
        public async Task Create(StoreRequest request)
        {
            await _storeRepository.Create(Mapper.Map<Store>(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task Update(UpdateStoreRequest request)
        {
            await _storeRepository.Update(Mapper.Map<Store>(request));
        }
    }
}