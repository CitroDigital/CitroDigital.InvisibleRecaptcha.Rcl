using CMS.FormEngine;
using Newtonsoft.Json;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Validation
{
    public class InvisibleRecaptchaResponse : RecaptchaResponse
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("score")]
        public double Score { get; set; }
    }
}