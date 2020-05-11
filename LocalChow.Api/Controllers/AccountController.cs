using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LocalChow.Api.Settings;
using LocalChow.Domain.DTO;
using LocalChow.Domain.Repository;
using LocalChow.Persistence.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LocalChow.Api.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(ILogger<AccountController> logger, IMapper mapper, UserManager<User> userManager, 
            IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authentication successful");
        }

        [HttpPost("{role}")]
        public async Task<IActionResult> Register(string role, [FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User object null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid User object");
                }
                var dbRole = _repositoryWrapper.Role.GetRoleByName(role);
                if (dbRole == null)
                {
                    return NotFound("Role not found");
                }
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                await _userManager.AddToRoleAsync(user, role);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong registering user", ex);
                return BadRequest("Failed to register user");
            }
        }
        

        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    return BadRequest("User object null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid User object");
                }
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.FindByEmailAsync(user.Email);
                if (result == null)
                {
                    return BadRequest("Invalid Email");
                }
                var passwordCheck = await _userManager.CheckPasswordAsync(result, userDto.Password);
                if (!passwordCheck)
                {
                    return BadRequest("Invalid Password");
                }
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));

                var authenticationConfig = new AuthenticationConfiguration();
                _configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authenticationConfig);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfig.SecreteKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: authenticationConfig.ValidIssuer,
                    audience: authenticationConfig.ValidAudience,
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong registering user", ex);
                return BadRequest($"Failed to login user, {ex}");
            }
        }
    }
}
