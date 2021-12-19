using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Base;
using CMS.Helpers;
using CitroDigital.InvisibleRecaptcha.Rcl.Helpers;
using CitroDigital.InvisibleRecaptcha.Rcl.Validation;
using Microsoft.AspNetCore.Mvc;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Controllers
{
    [Route("recaptcha/validation")]
    public class RecaptchaValidationController : Controller
    {
        public IActionResult Post(string action, string token)
        {
            var validationResultList = new List<ValidationResult>();

            var recaptchaValidator = new InvisibleRecaptchaValidator
            {
                PrivateKey = RecaptchaSettings.TryGetPrivateKey(),
                RemoteIP = RequestContext.UserHostAddress,
                Response = token
            };

            var recaptchaResponse = recaptchaValidator.Validate();
            if (recaptchaResponse != null)
            {
                if (!string.IsNullOrEmpty(recaptchaResponse.ErrorMessage))
                    validationResultList.Add(new ValidationResult(recaptchaResponse.ErrorMessage));
                if (action != null && !CMSString.Equals(action, recaptchaResponse.Action))
                    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.actioninvalid")));
                //if (recaptchaResponse.Score < Score)
                //    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.scoreinvalid")));
            }
            else
            {
                validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.serverunavailable")));
            }

            if (validationResultList.Any())
            {
                return BadRequest(validationResultList);
            }

            return Ok();
        }
    }
}
