using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LocalChow.Domain.DTO;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LocalChow.Api.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public RoleController(ILogger<AccountController> logger, IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost()]
        public IActionResult CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
                if (roleDto == null)
                {
                    return BadRequest("Role object null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Role object");
                }
                var role = _mapper.Map<Role>(roleDto);
                role.NormalizedName = roleDto.Name.ToUpper();
                _repositoryWrapper.Role.Create(role);
                _repositoryWrapper.Save();
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong creating role", ex);
                return BadRequest("Failed to create role");
            }
        }
    }
}
