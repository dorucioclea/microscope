using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace Microscope.Controllers_api
{
    [Route("api/Roles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleApiController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleApiController(RoleManager<IdentityRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAll()
        {
            return await this._roleManager.Roles.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityRole>> GetById(string id)
        {
            var role = await this._roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<IdentityRole>> Delete(string id)
        {
            var role = await this._roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            await this._roleManager.DeleteAsync(role);

            return role;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityRole>> Create([FromBody] string name)
        {
            var role = new IdentityRole();
            role.Name = name;
            
            var dto = await this._roleManager.CreateAsync(role);
            return role;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IdentityRole>> Update(string id, [FromBody] string name)
        {
            var role = await this._roleManager.FindByIdAsync(id);

            if(role == null)
            {
                return NotFound();
            }

            role.Name = name;
            await this._roleManager.UpdateAsync(role);

            return role;
        }
    }
}
