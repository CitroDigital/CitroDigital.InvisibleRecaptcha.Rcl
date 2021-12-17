using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents.InvisibleRecaptcha;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Helpers
{
    public static class HtmlHelperExtensions
    {

        public static string GenerateIdForRecaptcha(this IHtmlHelper<InvisibleRecaptchaComponent> helper)
        {
            var viewData = helper.ViewData;
            var model = helper.ViewData.Model;

            return viewData.TemplateInfo.GetFullHtmlFieldName(helper.GenerateIdFromName($"{model.Name}.{model.LabelForPropertyName}"));
        }

        public static void AddRecaptchaProperties(this IHtmlHelper<InvisibleRecaptchaComponent> helper, IDictionary<string, object> dictionary)
        {
            var viewData = helper.ViewData;
            var component = viewData.Model;

            dictionary.SetAttributeValue("id", GenerateIdForRecaptcha(helper));

            dictionary.SetAttributeValue("data-site-key", RecaptchaSettings.TryGetPublicKey());
            dictionary.SetAttributeValue("data-action", component.Action);
            dictionary.SetAttributeValue("data-recaptcha-validation-token", null);
        }
    }
}
