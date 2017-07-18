using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Models;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("[controller]")]    
    public class UsersController: BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var usrs = _userManager.Users.Where(x => x.UserName != "sa");            

            var dtos = usrs.Select(x => new UserDto
            {
                Username = x.UserName,
                Email = x.Email,                
                FirstName = x.FirstName,
                LastName = x.LastName,

            });

            return Ok(new { data = dtos });
        }


        //GET users/sa
        [HttpGet("{username}")]        
        public async Task<IActionResult> Get(string username)
        {
            var usr = await _userManager.FindByNameAsync(username);
            if (usr == null)
                return NotFound("No user found");
            var roles = await _userManager.GetRolesAsync(usr);
            var dto=new UserDto
            {
                Username = usr.UserName,
                Email = usr.Email,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Roles = string.Join(",", roles)
            };

            return Ok(dto);
        }

        //POST staffs
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody]UserDto value)
        {
            var usr = new ApplicationUser
            {
                UserName = value.Username,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                EmailConfirmed = false
            };
            var result=await _userManager.CreateAsync(usr, value.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return new ValidationFailedResult(ModelState);
            }
            await AssignRoles(usr, value.Roles);            

            return Ok();
        }        

        // PUT staffs/5
        [HttpPut("{username}")]
        public async Task Put(string username, [FromBody]UserDto value)
        {
            var usr=await _userManager.FindByNameAsync(username);
            usr.FirstName = value.FirstName;
            usr.LastName = value.LastName;
            usr.Email = value.Email;
            await _userManager.UpdateAsync(usr);
            await AssignRoles(usr, value.Roles);
        }


        // DELETE staffs/5
        [HttpDelete("{username}")]
        public async Task Delete(string username)
        {
            await _userManager.DeleteAsync(await _userManager.FindByNameAsync(username));
        }


        private async Task AssignRoles(ApplicationUser user, string roles)
        {
            var allowedRoles = new[] { "CUSTOMER", "ADMIN", "MERCHANT" };
            var assignedRoles = roles?.ToUpper().Split(',');
            var existingRoles = await _userManager.GetRolesAsync(user);

            foreach (var existingRole in existingRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, existingRole);
            }
            if (assignedRoles != null)
            {
                foreach (var role in assignedRoles)
                {
                    if (allowedRoles.Contains(role))
                        await _userManager.AddToRoleAsync(user, role);
                }
            }
        }

    }
}
