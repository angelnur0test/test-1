using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RVCA_base2.Helpers
{
    public static class PageBuilderHelper
    {
        public static string GetNullOrEmptyString(object input)
        {
            if (input is string)
            {
                if (string.IsNullOrEmpty(input as string))
                    return "-";
                else
                    return (input as string);
            }
            else if (input == null)
                return "-";
            else
                return input.ToString();
        }

        public static string GetActiveCss(HttpRequestBase request, string inputPath)
        {
            string[] alternatePaths = inputPath.Split('|');

            foreach (string path in alternatePaths)
            {
                var pathFixed = "/" + path.ToLowerInvariant().Replace("*", "/");
                if (request.Url.AbsolutePath.ToLowerInvariant().StartsWith(pathFixed))
                    return "class=\"active\"";
            }
            return "";
        }

        public static string GetActiveCssLi(HttpRequestBase request, string inputPath)
        {
            string[] alternatePaths = inputPath.Split('|');
            foreach (string path in alternatePaths)
            {
                var pathFixed = "/" + path.ToLowerInvariant().Replace("*", "/");
                if (request.Url.AbsolutePath.ToLowerInvariant().StartsWith(pathFixed))
                    return "class=\"active\"";
            }
            return "";
        }

        public static string GetActiveCssUl(HttpRequestBase request, string path)
        {
            path = "/" + path.ToLowerInvariant().Replace("*", "/");
            if (request.Url.AbsolutePath.ToLowerInvariant().StartsWith(path))
                return "aria-expanded=\"true\"";
            else
                return "aria-expanded=\"false\"";
        }

        public static string GetConfigValue(string configName)
        {
            if (ConfigurationManager.AppSettings[configName] != null)
            {
                return ConfigurationManager.AppSettings[configName];
            }
            return string.Empty;
        }

        public static string IsValueSelected(Dictionary<string, string> propertyBag, string propertyName, string selectValue,bool isDefault=false)
        {
            if (propertyBag == null)
            {
                if (isDefault) return "selected";
                return string.Empty;
            }
            string actualValue = string.Empty;
            propertyBag.TryGetValue(propertyName, out actualValue);
            if (!string.IsNullOrEmpty(actualValue))
            {
                List<string> allValues = actualValue.Split(',').ToList();
                if (allValues.Contains(selectValue))
                    return "selected";
            }
            else if (isDefault)
            {
                return "selected";
            }
            return "";
        }
    }
}