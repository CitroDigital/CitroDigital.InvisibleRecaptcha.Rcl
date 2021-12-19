using CitroDigital.InvisibleRecaptcha.Rcl.Helpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using static System.String;

namespace CitroDigital.InvisibleRecaptcha.Rcl.TagHelpers
{
    public class GoogleRecaptchaAssetsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            switch (context.TagName.ToLower())
            {
                case "body":
                {
                    AppendGoogleRecaptcha(output); 
                    break;
                }
                case "head":
                {
                    AppendGoogleRecaptchaHeadCode(output);
                    break;
                }
            }
        }

        /// <summary>
        /// Apply recaptcha stylesheet code to the head tag
        /// </summary>
        /// <param name="output"></param>
        private void AppendGoogleRecaptchaHeadCode(TagHelperOutput output)
        {
            var code = RecaptchaSettings.TryGetPublicKey();

            if (IsNullOrEmpty(code)) return;

            var recaptchaString = $@"
                <link rel=""stylesheet"" href=""/_content/CitroDigital.InvisibleRecaptcha.Rcl/Content/Bundles/Public/formComponents.min.css"" />
            ";

            output.PostContent.AppendHtml(recaptchaString);
        }

        /// <summary>
        /// Apply the loader script to the end of the body content
        /// </summary>
        /// <param name="output"></param>
        private void AppendGoogleRecaptcha(TagHelperOutput output)
        {
            var code = RecaptchaSettings.TryGetPublicKey();
            if (IsNullOrWhiteSpace(code)) return;

            var recaptchaString = $@"
                <script src=""https://www.google.com/recaptcha/api.js?render={code}""></script>
                <script src=""/_content/CitroDigital.InvisibleRecaptcha.Rcl/Content/Bundles/Public/formComponents.min.js""></script>
            ";

            output.PostContent.AppendHtml(recaptchaString);
        }
    }
}
