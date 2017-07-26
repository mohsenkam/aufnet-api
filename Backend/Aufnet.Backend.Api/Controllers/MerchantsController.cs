using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Api.ActionFilters;
using Aufnet.Backend.Api.Shared;
using Aufnet.Backend.Api.Validation;
using Aufnet.Backend.ApiServiceShared.Models.Merchant;
using Aufnet.Backend.ApiServiceShared.Shared;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/[controller]")]    
    public class MerchantsController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMerchantsService _merchantsService;

        public MerchantsController(UserManager<ApplicationUser> userManager, IMerchantsService merchantsService)
        {
            _userManager = userManager;
            _merchantsService = merchantsService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> Get()
        {
            return new string[] { "value111", "value2" };

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

        //POST 
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]MerchantsSignUpDto value)
        {
            //validation
            if (!RolesConstants.Roles.Contains(value.Role))
            {
                ModelState.AddModelError(ErrorCodesConstants.NotExistingRole.Code, ErrorCodesConstants.NotExistingRole.Message);
                return new ValidationFailedResult(ModelState);
            }

            //preparation

            //logic
            var result=await _merchantsService.SignUpAsync(value.Username, value.Password, value.Role);
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
