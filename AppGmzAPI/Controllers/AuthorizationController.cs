﻿using AppGmz.Models.AppSettings;
using AppGmz.Models.DtoModels;
using AppGmz.Models.IdentityModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppGmzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly AppSettings _optionsApp;
        private readonly IMapper _mapper;

        public AuthorizationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, ILogger<AuthorizationController> logger,
            IOptions<AppSettings> optionsApp, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
            _optionsApp = optionsApp.Value;
        }

        [Route("register")]
        [HttpPost]
        //POST : /api/Authorization/register
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            try
            {
                _logger.LogInformation(nameof(AuthorizationController.Register));
                if (TryValidateModel(userRegisterDto))
                {
                    var newUser = _mapper.Map<AppUser>(userRegisterDto);
                    var result = await _userManager.CreateAsync(newUser, userRegisterDto.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result);
                    }
                    if (userRegisterDto.UserName == "Super_Admin")
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            await _roleManager.CreateAsync(new AppRole() { Name = "Admin" });
                        }
                        await _userManager.AddToRoleAsync(newUser, "Admin");
                    }
                    else
                    {
                        if (!await _roleManager.RoleExistsAsync("User"))
                        {
                            await _roleManager.CreateAsync(new AppRole() { Name = "User" });
                        }
                        await _userManager.AddToRoleAsync(newUser, "User");
                    }
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, false);
                        return Ok(result);
                    }
                }
                _logger.LogError(nameof(AuthorizationController.Register));
                return BadRequest("Error!");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(AuthorizationController.Register), e);
                return BadRequest(e.Message);
            }
        }

        [Route("login")]
        [HttpPost]
        //POST : /api/Authorization/login
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                _logger.LogInformation(nameof(Login));
                if (TryValidateModel(userLoginDto))
                {
                    var findUser = await _userManager.FindByNameAsync(userLoginDto.UserName);
                    if (findUser != null &&
                        await _userManager.CheckPasswordAsync(findUser, userLoginDto.Password))
                    {
                        var token = CreateToken(findUser).Result;
                        return Ok(new { token });
                    }
                    _logger.LogError(nameof(AuthorizationController.Login));
                    return BadRequest("Error");
                }
                _logger.LogError(nameof(AuthorizationController.Login));
                return BadRequest("Error");
            }
            catch (Exception e)
            {
                _logger.LogError(nameof(AuthorizationController.Login), e);
                return BadRequest(e.Message);
            }
        }

        private async Task<string> CreateToken(AppUser userApp)
        {
            var role = await _userManager.GetRolesAsync(userApp);
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userApp.Id.ToString() ),
                    new Claim(new IdentityOptions().ClaimsIdentity.RoleClaimType,role.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_optionsApp.JWT_Secret)),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescription);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}