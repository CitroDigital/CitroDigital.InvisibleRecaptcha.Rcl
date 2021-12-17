using System;
using System.Linq;
using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents.InvisibleRecaptcha;
using CitroDigital.InvisibleRecaptcha.Rcl.Helpers;
using Kentico.Forms.Web.Mvc;
using Kentico.Forms.Web.Mvc.Widgets;

namespace CitroDigital.InvisibleRecaptcha.Rcl
{
    internal static class InvisibleRecaptchaConfiguration
    {
        public static void InjectFormMarkup(object sender, GetFormWidgetRenderingConfigurationEventArgs e)
        {
            if (e.FormComponents.Any(component => component.Definition.Identifier == InvisibleRecaptchaComponent.IDENTIFIER))
            { 
                e.Configuration.SubmitButtonHtmlAttributes.SetAttributeValue("data-validate-recaptcha", null);
            }
        }

        public static void InjectComponentMarkup(object sender, GetFormFieldRenderingConfigurationEventArgs e)
        {
            if (e.FormComponent is InvisibleRecaptchaComponent)
            {
                e.Configuration.RootConfiguration.HtmlAttributes.SetClassValue("invisible-recaptcha-wrapper");
            }
        }
    }
}
