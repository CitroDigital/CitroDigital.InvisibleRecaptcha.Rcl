using CMS.Core;
using CMS.DataEngine;
using CMS.SiteProvider;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Helpers
{
    internal static class RecaptchaSettings
    {
        public static bool UseRecaptchaV3 =>
            SettingsKeyInfoProvider.GetBoolValue("RecaptchaV3Enabled", SiteContext.CurrentSiteID);

        public static string TryGetPublicKey()
        {
            var settingsKey =
                SettingsKeyInfoProvider.GetValue("CMSRecaptchaPublicKey", SiteContext.CurrentSiteID);

            if (string.IsNullOrWhiteSpace(settingsKey))
                settingsKey = Service.Resolve<IAppSettingsService>()["CMSRecaptchaPublicKey"];

            return string.IsNullOrWhiteSpace(settingsKey) ? null : settingsKey;
        }

        public static string TryGetPrivateKey()
        {
            var settingsKey =
                SettingsKeyInfoProvider.GetValue("CMSRecaptchaPrivateKey", SiteContext.CurrentSiteID);

            if (string.IsNullOrWhiteSpace(settingsKey))
                settingsKey = Service.Resolve<IAppSettingsService>()["CMSRecaptchaPrivateKey"];

            return string.IsNullOrWhiteSpace(settingsKey) ? null : settingsKey;
        }
    }
}
