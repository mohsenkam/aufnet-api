﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Aufnet.Backend.ApiServiceShared.Models.Merchant
{
    public class MerchantSignUpDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public string Password { get; set; }
        //[Required]
        //public string Username { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //[Required]
        //public string Password { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        //public string Role { get; set; }

    }
}
