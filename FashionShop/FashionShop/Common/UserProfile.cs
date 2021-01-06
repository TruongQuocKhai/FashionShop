using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FashionShop.Common
{
    public class UserProfile
    {
       // [JsonProperty("id")]
        public int UserId { get; set; }
        [JsonProperty("email")]
        public string UserEmail { get; set; }
        //[JsonProperty("given_name")]
        //public string DisplayName { get; set; }
        public string GroupID { get; set; }

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }

        [JsonProperty("given_name")]     // Ten
        public string GivenName { get; set; }

        [JsonProperty("family_name")]   // Ho
        public string FamilyName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}