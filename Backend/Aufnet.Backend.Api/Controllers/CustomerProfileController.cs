using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/customers/{username}/profile")]
    public class CustomerProfileController: BaseController
    {

        // GET api/customers/john/customerprofile
        
        [HttpGet]
        public IEnumerable<string> Get(string username)
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/customers/john/customerprofile
        [HttpPost]
        public void Post([FromBody]CustomerProfileDto value)
        {
        }

        // PUT api/customers/john/customerprofile
        [HttpPut]
        public void Put(string username, [FromBody]CustomerProfileDto value)
        {
        }
    }
}
