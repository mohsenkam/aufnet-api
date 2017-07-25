﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.ApiServiceShared.Models
{
    public class CustomerSignUpDto
    {

        [Required]
        public string Username { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Password { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

    }
}