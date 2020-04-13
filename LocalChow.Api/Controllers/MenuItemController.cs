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
    [Route("api/menu-item")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MenuItemController(IRepositoryWrapper repositoryWrapper, ILogger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllMenuItems()
        {
            try
            {
                var menuItem = _repositoryWrapper.MenuItem.GetAllMenuItems();
                if (menuItem == null || !menuItem.Any())
                {
                    _logger.LogError("No menu items found in db");
                    return NotFound();
                }
                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllMenuItems, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMenuItem(int id)
        {
            try
            {
                var menuItem = _repositoryWrapper.MenuItem.GetMenuItemById(id);
                if (menuItem == null)
                {
                    _logger.LogError($"No menu item found in db with id:{id}");
                    return NotFound($"No menu item found with id:{id}");
                }
                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMenuItem, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateMenuItem([FromBody] MenuItem menuItem)
        {
            try
            {
                if (menuItem == null)
                {
                    return BadRequest("Store object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repositoryWrapper.MenuItem.Create(menuItem);
                _repositoryWrapper.Save();
                return CreatedAtRoute("CreateStore", menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateMenuItem, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMenuItem(int id, [FromBody] MenuItem menuItem)
        {
            try
            {
                if (menuItem == null)
                {
                    return BadRequest("MenuItem object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid menuItem object");
                }
                var dbMenuItem = _repositoryWrapper.MenuItem.GetMenuItemById(id);
                if (dbMenuItem == null)
                {
                    return NotFound($"No menu item found with id:{id}");
                }
                _repositoryWrapper.MenuItem.Update(menuItem);
                _repositoryWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside update menu item, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenuItem(int id)
        {
            try
            {
                var dbMenuItem = _repositoryWrapper.MenuItem.GetMenuItemById(id);
                if (dbMenuItem == null)
                {
                    return NotFound($"No menu found with id:{id}");
                }
                _repositoryWrapper.MenuItem.Delete(dbMenuItem);
                _repositoryWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside delete menu, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }
    }
}
