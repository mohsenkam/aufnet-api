using System;
using Aufnet.Backend.ApiServiceShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/[controller]")]
    public class CustomerProfilesController: BaseController
    {

        // GET api/customerprofiles/john
        [HttpGet("{username}")]
        public CustomerProfileDto Get(string username)
        {
            throw new NotImplementedException();
        }

        // POST api/customerprofiles
        [HttpPost]
        public void Post([FromBody]CustomerProfileDto value)
        {
        }

        // PUT api/customerprofiles/john
        [HttpPut("{username}")]
        public void Put(string username, [FromBody]CustomerProfileDto value)
        {
        }
    }
}
