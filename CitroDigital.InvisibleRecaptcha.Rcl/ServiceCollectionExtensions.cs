using CMS;
using CitroDigital.InvisibleRecaptcha.Rcl.TagHelpers;
using Kentico.Forms.Web.Mvc;
using Kentico.Forms.Web.Mvc.Widgets;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

[assembly: AssemblyDiscoverable]

namespace CitroDigital.InvisibleRecaptcha.Rcl
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="GoogleRecaptchaAssetsTagHelper"/> to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGoogleRecaptchaV3(this IServiceCollection services)
        {
            services.AddSingleton<ITagHelperComponent, GoogleRecaptchaAssetsTagHelper>();

            // Configure Form Fields
            FormWidgetRenderingConfiguration.GetConfiguration.Execute += InvisibleRecaptchaConfiguration.InjectFormMarkup;

            FormFieldRenderingConfiguration.GetConfiguration.Execute += InvisibleRecaptchaConfiguration.InjectComponentMarkup;

            return services;
        }
    }
}
