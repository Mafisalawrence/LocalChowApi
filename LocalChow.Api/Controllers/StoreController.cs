using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LocalChow.Api.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ILogger<StoreController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public StoreController(IRepositoryWrapper repositoryWrapper, ILogger<StoreController> logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            try
            {
                var stores = _repositoryWrapper.Store.FindAll();
                if (stores == null || !stores.Any())
                {
                    _logger.LogError("No stores found in db");
                    return NotFound();
                }
                return Ok(stores);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getAllStores, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStore(int id)
        {
            try
            {
                var store = _repositoryWrapper.Store.GetStoreById(id);
                if (store == null)
                {
                    _logger.LogError($"No menu found in db with id:{id}");
                    return NotFound($"No menu found with id:{id}");
                }
                return Ok(store);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getStore, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody] Store store)
        {
            try
            {
                if (store == null)
                {
                    return BadRequest("Store object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repositoryWrapper.Store.Create(store);
                _repositoryWrapper.Save();
                return CreatedAtRoute("CreateStore", store);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateStore, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore(int id, [FromBody] Store store)
        {
            try
            {
                if (store == null)
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
