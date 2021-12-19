using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CMS.Base;
using CMS.Core;
using CMS.Helpers;
using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents.InvisibleRecaptcha;
using CitroDigital.InvisibleRecaptcha.Rcl.Helpers;
using CitroDigital.InvisibleRecaptcha.Rcl.Validation;
using Kentico.Forms.Web.Mvc;

[assembly: RegisterFormComponent(
    InvisibleRecaptchaComponent.IDENTIFIER,
    typeof(InvisibleRecaptchaComponent),
    "Invisible Recaptcha",
    Description = "{$Google.InvisibleRecaptcha.Name$}",
    IconClass = "icon-recaptcha",
    ViewName = "~/Components/FormComponents/InvisibleRecaptcha/_Default.cshtml"
)]

namespace CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents.InvisibleRecaptcha
{
    public class InvisibleRecaptchaComponent : FormComponent<InvisibleRecaptchaComponentProperties, string>
    {
        public const string IDENTIFIER = "Google.Recaptcha.Invisible";

        private bool? mSkipRecaptcha;
        private static readonly Regex mRegex = new Regex("[^a-zA-Z_]");

        [BindableProperty]
        public string Value { get; set; }

        public string Action
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Properties.Action))
                {
                    return this.GetBizFormComponentContext()?.FormInfo.FormName;
                }

                return mRegex.Replace(Properties.Action, string.Empty);
            }
        }

        public double Score
        {
            get
            {
                if (double.TryParse(Properties.Score, out var score))
                {
                    return score;
                }
                //Default score
                return 0.5;
            }
        }

        public bool IsConfigured => AreKeysConfigured && !SkipRecaptcha;

        private bool SkipRecaptcha
        {
            get
            {
                mSkipRecaptcha ??= ValidationHelper.GetBoolean(Service.Resolve<IAppSettingsService>()["RecaptchaSkipValidation"], false);
                return mSkipRecaptcha.Value;
            }
        }

        private bool AreKeysConfigured => !string.IsNullOrEmpty(RecaptchaSettings.TryGetPublicKey()) && !string.IsNullOrEmpty(RecaptchaSettings.TryGetPrivateKey());

        public override string LabelForPropertyName => string.Empty;

        public override string GetValue()
        {
            return string.Empty;
        }

        public override void SetValue(string value)
        {
            Value = string.Empty;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResultList = new List<ValidationResult>();
            validationResultList.AddRange(base.Validate(validationContext));
            if (!IsConfigured | VirtualContext.IsInitialized)
                return (IEnumerable<ValidationResult>)validationResultList;
            var recaptchaValidator = new InvisibleRecaptchaValidator
            {
                PrivateKey = RecaptchaSettings.TryGetPrivateKey(),
                RemoteIP = RequestContext.UserHostAddress,
                Response = Value
            };

            var recaptchaResponse = recaptchaValidator.Validate();
            if (recaptchaResponse != null)
            {
                if (!string.IsNullOrEmpty(recaptchaResponse.ErrorMessage))
                    validationResultList.Add(new ValidationResult(recaptchaResponse.ErrorMessage));
                if (Action != null && !CMSString.Equals(Action, recaptchaResponse.Action))
                    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.actioninvalid")));
                if (recaptchaResponse.Score < Score)
                    validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.scoreinvalid")));
            }
            else
            {
                validationResultList.Add(new ValidationResult(ResHelper.GetString("recaptcha.error.serverunavailable")));
            }

            return validationResultList;
        }
    }
}
