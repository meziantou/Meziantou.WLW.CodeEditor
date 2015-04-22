using System.Collections;
using WindowsLive.Writer.Api;

namespace Meziantou.WLW.CodeEditor
{
    public static class SmartContentExtensions
    {
        public static string[] Languages = { "C", "C++", "C#", "VB.NET", "XML", "XAML" };

        public static string GetCode(this ISmartContent smartContent)
        {
            return smartContent.Properties["code"];
        }

        public static void SetCode(this ISmartContent smartContent, string value)
        {
            smartContent.Properties["code"] = value;
        }
        public static string GetLanguage(this ISmartContent smartContent)
        {
            return smartContent.Properties["language"];
        }

        public static void SetLanguage(this ISmartContent smartContent, string value)
        {
            smartContent.Properties["language"] = value;
        }
    }
}