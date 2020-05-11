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
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public OrderController(IRepositoryWrapper repositoryWrapper, ILogger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var orders = _repositoryWrapper.Store.FindAll();
                if (orders == null || !orders.Any())
                {
                    _logger.LogError("No orders found in db");
                    return NotFound();
                }
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll orders, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(Guid id)
        {
            try
            {
                var order = _repositoryWrapper.Order.GetOrderById(id);
                if (order == null)
                {
                    _logger.LogError($"No order found in db with id:{id}");
                    return NotFound($"No order found with id:{id}");
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOrder, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repositoryWrapper.Order.Create(order);
                _repositoryWrapper.Save();
                return CreatedAtRoute("CreateOrder", order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOrder, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }
    }
}
