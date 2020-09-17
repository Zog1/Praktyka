using CarRental.API.Attributes;
using CarRental.API.Resources;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [AuthorizeEnumRoles(RoleOfWorker.Admin)]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        public ResourceManager resourcesManager;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }
        /// <summary>
        /// Get all users return list of users
        /// method avaible only for admin
        /// </summary>
        /// <returns> /// return 200 
        /// If list is null return 404(NotFound)
        /// if not admin return 401</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await usersService.GetAllUsersAsync();
            if (result == null)
            {
                return NotFound(resourcesManager.GetString("DatabaseEmpty"));
            }
            return Ok(result);
        }
        /// <summary>
        /// Get user with exciting Id
        /// Method only for Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return 200
        /// If id not exciting return 404(NotFound)</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {

            var user = await usersService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            user = await usersService.GetUserAsync(id);
            return Ok(user);
        }
        /// <summary>
        /// Delete user with exciting Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return 200
        /// with not exciting Id retutn 404(NotFound)</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {

            if (!await usersService.DeleteUserAsync(id))
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            return Ok();
        }
        /// <summary>
        /// Update User with correct model and id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefectDto"></param>
        /// <returns>return 200 and Update User
        /// if id not correct return 404(NotFound)
        /// or user null return 404(NotFound)</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, UsersDto usersDto)
        {
            if (id != usersDto.UserId)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            var result = await usersService.UpdateUserAsync(usersDto);
            if (result.isValid == false)
            {
                return NotFound(resourcesManager.GetString("NotFound"));
            }
            return Ok(result);
        }
    }
}