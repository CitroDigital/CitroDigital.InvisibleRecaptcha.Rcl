using System.Collections.Generic;
using CMS.FormEngine;
using CitroDigital.InvisibleRecaptcha.Rcl.Components.FormComponents;
using Kentico.Forms.Web.Mvc;

namespace CitroDigital.InvisibleRecaptcha.Rcl.Helpers
{
    public static class DictionaryHelper
    {
        public static void SetClassValue(this IDictionary<string, object> dictionary, string value)
        {
            if (dictionary.ContainsKey("class"))
            {
                dictionary["class"] += $" {value}";
            }
            else
            {
                dictionary["class"] = value;
            }
        }

        public static void SetAttributeValue(this IDictionary<string, object> dictionary, string key, string value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += $" {value}";
            }
            else
            {
                dictionary[key] = value;
            }
        }


    }
}
