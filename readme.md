# Invisible Recaptcha Form Component for Net 5.0
Adds the [Google Invisible Recaptcha](https://developers.google.com/recaptcha/docs/v3) form component to your MVC Site.

# Installation
1. Install the `CitroDigital.InvisibleRecaptcha.Rcl` [Nuget Package](https://www.nuget.org/packages/CitroDigital.InvisibleRecaptcha.Rcl/) to your MVC Site


# Configuration
1. Configure reCAPTCHA
2. Go to https://www.google.com/recaptcha/admin and sign in with your Google account.
3. Select the reCAPTCHA v2 type (other reCAPTCHA types are not supported by default).
4. Fill in all required details, including the domain where your site is running (the presentation domain of your MVC live site).
5. Copy your Site key and Secret key.
2. In the Kentico Admin interface
6. Open the Settings application in the administration interface.
7. Navigate to the Security & Membership -> Protection settings category.
8. Under CAPTCHA settings, paste the API keys into the reCAPTCHA site key and reCAPTCHA secret key settings respectively.
9. Save the settings.
```csharp
    using CitroDigital.InvisibleRecaptcha.Rcl;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //... ommited for brevity

            services.AddGoogleRecaptchaV3();
                        
            //... ommited for brevity
        }
    }
```
11. Drag the Invisible reCAPTCHA form component in the Form Builder
12. Configure the Action and Score Properties
13. Default Score if left blank is 0.5
14. Default Action if left blank is the FormName

InvisibleRecaptcha Attribute has two properties Action and Score.

1. Action
  1. reCAPTCHA v3 introduces a new concept: actions. When you specify an action name in each place you execute reCAPTCHA, you enable the following new features: A detailed break-down of data for your top ten actions in the admin console Adaptive risk analysis based on the context of the action, because abusive behavior can vary. Importantly, when you verify the reCAPTCHA response, you should verify that the action name is the name you expect.
2. Score
  1. reCAPTCHA v3 returns a score (1.0 is very likely a good interaction, 0.0 is very likely a bot). Based on the score, you can take variable action in the context of your site. Every site is different, but below are some examples of how sites use the score. As in the examples below, take action behind the scenes instead of blocking traffic to better protect your site.

### Todo
1. Add support and validation for reCaptcha use outside of Kentico Forms - ie) newsletter forms, search queries, button clicks events.
2. Add support for Net Framework

# Issues/Support
You can submit bugs through the issue list and we will get to them as soon as we can.