using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents.InvisibleRecaptcha;
using CitroDigital.InvisibleRecaptcha.Rcl.Helpers;
using Kentico.Forms.Web.Mvc;
using Kentico.Forms.Web.Mvc.Widgets;
using System.Linq;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Configuration
{
    internal static class InvisibleRecaptchaConfiguration
    {
        public static void InjectFormMarkup(object sender, GetFormWidgetRenderingConfigurationEventArgs e)
        {
            //add attribute to submit button if form contains invisible recaptcha form control
            if (e.FormComponents.Any(component => component.Definition.Identifier == InvisibleRecaptchaComponent.IDENTIFIER))
            { 
                e.Configuration.SubmitButtonHtmlAttributes.SetAttributeValue("data-validate-recaptcha", null);
            }
        }

        public static void InjectComponentMarkup(object sender, GetFormFieldRenderingConfigurationEventArgs e)
        {
            //add class to form component to hide
            if (e.FormComponent is InvisibleRecaptchaComponent)
            {
                e.Configuration.RootConfiguration.HtmlAttributes.SetClassValue("invisible-recaptcha-wrapper");
            }
        }
    }
}
