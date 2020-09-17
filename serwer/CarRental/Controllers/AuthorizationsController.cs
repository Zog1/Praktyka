using CarRental.API.Resources;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Resources;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/authorization")]
    [ApiController]
    public class AuthorizationsController : Controller
    {
        private readonly IAuthorizationService authorizationService;
        public  ResourceManager resourcesManager;
        public AuthorizationsController(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }

        /// <summary>
        /// Register user 
        /// Check if input email exciting in database
        /// If yes then return BadRequest
        /// else return Ok(User)
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>404(Not found) or
        /// 200(created) with new User </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return NotFound(resourcesManager.GetString("UserNull"));
            }
            var user = await authorizationService.RegistrationUserAsync(createUserDto);
            if (user == null)
            {
                return BadRequest(resourcesManager.GetString("EmailExiciting"));
            }
            return Ok(user);
        }
        /// <summary>
        /// Sign In user or admin , method avaible for anyone 
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>return 200 and access token and refresh
        /// or return 401(UnAuthorized) Email/Password not correct</returns>
        [HttpPost("signIn")]
        public async Task<IActionResult> SignInAsync(UserLoginDto userLoginDto)
        {
            var signInResult = await authorizationService.SignInAsync(userLoginDto);
            if (signInResult.Code == (int)HttpStatusCode.Unauthorized)
            {
                return Unauthorized(resourcesManager.GetString("EmailPassword"));
            }
            return Ok(signInResult);
        }
        /// <summary>
        /// Set up password , method will be work if you have 
        /// correct code of verification 
        /// </summary>
        /// <param name="updateUserPassword"></param>
        /// <returns>then return 200 and 
        /// save your password in database
        /// else return 404(NotFound) bad cod of verification</returns>
        [HttpPatch]
        public async Task<IActionResult> SetPasswordAsync(UpdateUserPasswordDto updateUserPassword)
        {
            if (!await authorizationService.SetPasswordAsync(updateUserPassword))
            {
                return NotFound(resourcesManager.GetString("CodeVerification"));
            }
            return Ok();
        }
    }
}