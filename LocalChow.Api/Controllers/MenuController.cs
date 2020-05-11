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
    [Route("api/Menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public MenuController(IRepositoryWrapper repositoryWrapper, ILogger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllMenus()
        {
            try
            {
                var menus = _repositoryWrapper.Menu.GetAllMenu();
                if (menus == null || !menus.Any())
                {
                    _logger.LogError("No menus found in db");
                    return NotFound();
                }
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside getAllMenus, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMenu(Guid id)
        {
            try
            {
                var menu = _repositoryWrapper.Menu.GetMenuById(id);
                if (menu == null)
                {
                    _logger.LogError($"No menu found in db with id:{id}");
                    return NotFound($"No menu found with id:{id}");
                }
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetMenu, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult CreateMenu([FromBody] Menu menu)
        {
            try
            {
                if (menu == null)
                {
                    return BadRequest("Menu object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repositoryWrapper.Menu.Create(menu);
                _repositoryWrapper.Save();
                return CreatedAtRoute("CreateMenu", menu);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside create menu, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateMenu(Guid id, [FromBody] Menu menu)
        {
            try
            {
                if (menu == null)
                {
                    return BadRequest("Menu object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid menu object");
                }
                var dbMenu = _repositoryWrapper.Menu.GetMenuById(id);
                if (dbMenu == null)
                {
                    return NotFound($"No menu found with id:{id}");
                }
                _repositoryWrapper.Menu.Update(menu);
                _repositoryWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside update menu, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(Guid id)
        {
            try
            {
                var dbMenu = _repositoryWrapper.Menu.GetMenuById(id);
                if (dbMenu == null)
                {
                    return NotFound($"No menu found with id:{id}");
                }
                _repositoryWrapper.Menu.Delete(dbMenu);
                _repositoryWrapper.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside update menu, exception:{ex.Message}");
                return StatusCode(500, $"Internal server error, exception : {ex.Message}");
            }
        }
    }
}
