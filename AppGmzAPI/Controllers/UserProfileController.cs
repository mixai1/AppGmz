using AppGmz.Models.DtoModels;
using AppGmz.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppGmzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserProfileController> _logger;

        public UserProfileController(UserManager<AppUser> userManager, ILogger<UserProfileController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [Authorize]
        [Route("userProfile")]
        [HttpGet]
        // Get : /api/UserProfile/userProfile
        public async Task<UserProfileDto> GetUserProfile()
        {
            try
            {
                //object User from HttpContext
                string userId = User.Claims.FirstOrDefault(u => u.Type == "UserId")?.Value;
                if (userId == null)
                {
                    return new UserProfileDto();
                }
                var user = await _userManager.FindByIdAsync(userId);
                // take listRoles for user
                var listRoles = await _userManager.GetRolesAsync(user);
                var profileUser = new UserProfileDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    ListRoles = listRoles
                };
                Log.Information(nameof(UserProfileController.GetUserProfile));
                return profileUser;
            }
            catch (Exception e)
            {
                Log.Error(nameof(UserProfileController.GetUserProfile), e);
                _logger.LogError(nameof(UserProfileController.GetUserProfile), e);
                return new UserProfileDto();
            }
        }
    }
}