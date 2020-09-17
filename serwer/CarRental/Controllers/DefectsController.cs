using CarRental.API.Attributes;
using CarRental.API.Resources;
using CarRental.DAL.Entities;
using CarRental.Services.Interfaces;
using CarRental.Services.Models.Defect;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using System.Threading.Tasks;

namespace CarRental.API.Controllers
{
    [Route("api/defects")]
    [ApiController]
    public class DefectsController : Controller
    {
        private readonly IDefectsService defectsService;
        public ResourceManager resourcesManager;
        public DefectsController(IDefectsService defectsService)
        {
            this.defectsService = defectsService;
            resourcesManager = new ResourceManager("CarRental.API.Resources.ResourceFile", typeof(ResourceFile).Assembly);
        }
        /// <summary>
        /// Register defect if model not empty and
        /// user and car not null 
        /// </summary>
        /// <param name="registerDefectDto"></param>
        /// <returns>return 200 defect
        /// else return 400(BadRequest) then model empty
        /// or user/car not found</returns>
        [HttpPost]
        [AuthorizeEnumRoles(RoleOfWorker.Admin, RoleOfWorker.Worker)]
        public async Task<IActionResult> RegisterDefectAsync(RegisterDefectDto registerDefectDto)
        {
            if (registerDefectDto == null)
            {
                return BadRequest(resourcesManager.GetString("Model"));
            }
            var register_defect = await defectsService.RegisterDefectAsync(registerDefectDto);
            if (register_defect == null)
            {
                return NotFound("ObjectCarUser");
            }
            return Ok(register_defect);
        }
        /// <summary>
        /// Get all defects return list of defects
        /// method avaible only for admin
        /// </summary>
        /// <returns> /// return 200 
        /// If list is null return 404(NotFound)
        /// if not admin return 401</returns>
        [HttpGet]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetAllDefectsAsync()
        {
            var defects = await defectsService.GetAllDefectsAsync();
            if (defects == null)
            {
                return NotFound(resourcesManager.GetString("DatabaseEmpty"));
            }
            return Ok(defects);
        }
        /// <summary>
        /// Get defect with exciting Id
        /// Method only for Admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return 200
        /// If id not exciting return 404(NotFound)</returns>
        [HttpGet("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> GetDefectAsync(int id)
        {
            var defect = await defectsService.GetDefectAsync(id);
            if (defect == null)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            return Ok(defect);
        }
        /// <summary>
        /// Update Defect with correct model and id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDefectDto"></param>
        /// <returns>return 200 and Update Defect
        /// if id not correct return 404(NotFound)
        /// or defect null return 404(NotFound)</returns>
        [HttpPatch("{id}")]
        [AuthorizeEnumRoles(RoleOfWorker.Admin)]
        public async Task<IActionResult> UpdateDefectAsync(int id, UpdateDefectDto updateDefectDto)
        {
            if (id != updateDefectDto.Id)
            {
                return NotFound(resourcesManager.GetString("NotExist"));
            }
            var defect = await defectsService.UpdateDefectAsync(updateDefectDto);
            if (defect == null)
            {
                return NotFound(resourcesManager.GetString("NotFound"));
            }
            return Ok(defect);
        }
    }
}