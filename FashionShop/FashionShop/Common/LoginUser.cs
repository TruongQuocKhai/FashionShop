using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace FashionShop.Common
{
    public class LoginUser
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }
        [JsonProperty("user_email")]
        public string UserEmail { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        public string GroupID { get; set; }

        

        [JsonProperty("verified_email")]
        public bool VerifiedEmail { get; set; }
     

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
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

