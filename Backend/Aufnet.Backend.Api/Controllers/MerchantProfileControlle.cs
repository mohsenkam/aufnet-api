using System;
using System.Collections.Generic;
using Aufnet.Backend.ApiServiceShared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aufnet.Backend.Api.Controllers
{
    [Route("api/merchants/{username}/profile")]
    public class MerchantProfileControlle : BaseController
    {

        // GET api/merchants/golnar/merchantprofile
        
        [HttpGet]
        public IEnumerable<string> Get(string username)
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/merchants/golnar/merchantprofile
        [HttpPost]
        public void Post([FromBody]CustomerProfileDto value)
        {
        }

        // PUT api/merchants/golnar/merchantprofile
        [HttpPut]
        public void Put(string username, [FromBody]CustomerProfileDto value)
        {
        }
    }
}
