using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalChow.Domain.DTO;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LocalChow.Api.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public StoreController(IRepositoryWrapper repositoryWrapper, ILogger<StoreController> logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            try
            {
                var stores = _repositoryWrapper.Store.FindAll().ToList();
                if (stores == null || !stores.Any())
                {
                    _logger.LogError("No stores found in db");
                    return NotFound();
                }
                var storeDto = _mapper.Map<IEnumerable<StoreDto>>(stores);
                return Ok(storeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getAllStores, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStore(Guid id)
        {
            try
            {
                var store = _repositoryWrapper.Store.GetStoreById(id);
                if (store == null)
                {
                    _logger.LogError($"No menu found in db with id:{id}");
                    return NotFound($"No menu found with id:{id}");
                }
                var storeDto = _mapper.Map<StoreDto>(store);
                return Ok(storeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getStore, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody] StoreDto storeRequest)
        {
            try
            {
                if (storeRequest == null)
                {
                    return BadRequest("Store object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                //var user = _repositoryWrapper.User.GetUserByID(storeRequest.UserID);
                //if (user == null)
                //{
                //    return NotFound($"User with Id:{storeRequest.UserID} not found");
                //}
                var store = _mapper.Map<Store>(storeRequest);
                //store.UserID = storeRequest.UserID;
                _repositoryWrapper.Store.Create(store);
                _repositoryWrapper.Save();
                return Ok(store.StoreID);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStore, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore(Guid id, [FromBody] StoreDto storeRequest)
        {
            try
            {
                if (storeRequest == null)
                {
                    return BadRequest("Store object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid store object");
                }
                var dbStore = _repositoryWrapper.Store.GetStoreById(id);
                if (dbStore == null)
                {
                    return NotFound($"No store found with id:{id}");
                }
                var store = _mapper.Map<Store>(storeRequest);
                store.StoreID = id;
                _repositoryWrapper.Store.Update(store);
                _repositoryWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside delete store, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }
    }
}
