using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Aufnet.Backend.ApiServiceShared
{
    public class Config
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrl => _configuration["Urls:BaseUrl"];
    }
}
