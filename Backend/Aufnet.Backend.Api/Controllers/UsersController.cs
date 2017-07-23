using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Models;
using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("[controller]")]    
    public class UsersController: BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UsersController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var usrs = _userManager.Users.Where(x => x.UserName != "sa");            

        //    var dtos = usrs.Select(x => new UserDto
        //    {
        //        Username = x.UserName,
        //        Email = x.Email,                
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,

        //    });

        //    return Ok(new { data = dtos });
        //}


        ////GET users/sa
        //[HttpGet("{username}")]        
        //public async Task<IActionResult> Get(string username)
        //{
        //    var usr = await _userManager.FindByNameAsync(username);
        //    if (usr == null)
        //        return NotFound("No user found");
        //    var roles = await _userManager.GetRolesAsync(usr);
        //    var dto=new UserDto
        //    {
        //        Username = usr.UserName,
        //        Email = usr.Email,
        //        FirstName = usr.FirstName,
        //        LastName = usr.LastName,
        //        Roles = string.Join(",", roles)
        //    };

        //    return Ok(dto);
        //}

        //POST use
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Post([FromBody]UserDto value)
        {
            //validation
            if (!RolesConstants.Roles.Contains(value.Role))
            {
                ModelState.AddModelError(ErrorCodesConstants.NotExistingRole.Code, ErrorCodesConstants.NotExistingRole.Message);
            }

            //preparation

            //logic
            var result=await _userService.SignUpAsync(value.Username, value.Password, value.Role);
            if (result.HasError())
            {
                foreach (var error in result.GetErrors())
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return new ValidationFailedResult(ModelState);
            }

            return Ok();
        }        

        //// PUT users/5
        //[HttpPut("{username}")]
        //public async Task Put(string username, [FromBody]UserDto value)
        //{
        //    var usr=await _userManager.FindByNameAsync(username);
        //    usr.FirstName = value.FirstName;
        //    usr.LastName = value.LastName;
        //    usr.Email = value.Email;
        //    await _userManager.UpdateAsync(usr);
        //    await AssignRoles(usr, value.Roles);
        //}


        // DELETE staffs/5
        [HttpDelete("{username}")]
        public async Task Delete(string username)
        {
            await _userManager.DeleteAsync(await _userManager.FindByNameAsync(username));
        }


        

    }
}
