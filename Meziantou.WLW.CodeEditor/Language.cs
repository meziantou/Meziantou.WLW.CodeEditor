using System;
using System.Collections.Generic;

namespace Meziantou.WLW.CodeEditor
{
    public class Language
    {
        public static IList<Language> DefaultLanguages;

        static Language()
        {
            DefaultLanguages = new List<Language>();
            DefaultLanguages.Add(new Language("ActionScript", "actionscript"));
            DefaultLanguages.Add(new Language("Apache Configuration", "apacheconf"));
            DefaultLanguages.Add(new Language("AppleScript", "applescript"));
            DefaultLanguages.Add(new Language("ASP.NET", "aspnet"));
            DefaultLanguages.Add(new Language("AutoHotkey", "autohotkey"));
            DefaultLanguages.Add(new Language("Bash", "bash"));
            DefaultLanguages.Add(new Language("C-like", "clike"));
            DefaultLanguages.Add(new Language("C", "c"));
            DefaultLanguages.Add(new Language("C++", "cpp"));
            DefaultLanguages.Add(new Language("C#", "csharp"));
            DefaultLanguages.Add(new Language("CSS", "css"));
            DefaultLanguages.Add(new Language("CoffeeScript", "CoffeeScript"));
            DefaultLanguages.Add(new Language("Dart", "dart"));
            DefaultLanguages.Add(new Language("Eiffel", "eiffel"));
            DefaultLanguages.Add(new Language("Erlang", "erlang"));
            DefaultLanguages.Add(new Language("F#", "fsharp"));
            DefaultLanguages.Add(new Language("Fortran", "fortran"));
            DefaultLanguages.Add(new Language("Gherkin", "gherkin"));
            DefaultLanguages.Add(new Language("Git", "git"));
            DefaultLanguages.Add(new Language("Go", "go"));
            DefaultLanguages.Add(new Language("Groovy", "groovy"));
            DefaultLanguages.Add(new Language("Haml", "haml"));
            DefaultLanguages.Add(new Language("Handlebars", "handlebars"));
            DefaultLanguages.Add(new Language("Haskell", "haskell"));
            DefaultLanguages.Add(new Language("HTML", "html"));
            DefaultLanguages.Add(new Language("Ini", "ini"));
            DefaultLanguages.Add(new Language("Jade", "jade"));
            DefaultLanguages.Add(new Language("Java", "java"));
            DefaultLanguages.Add(new Language("JavaScript", "javascript"));
            DefaultLanguages.Add(new Language("Latex", "latex"));
            DefaultLanguages.Add(new Language("Less", "less"));
            DefaultLanguages.Add(new Language("Markdown", "markdown"));
            DefaultLanguages.Add(new Language("Markup", "markup"));
            DefaultLanguages.Add(new Language("MATLAB", "matlab"));
            DefaultLanguages.Add(new Language("NASM", "Nasm"));
            DefaultLanguages.Add(new Language("NSIS", "nsis"));
            DefaultLanguages.Add(new Language("Objective-C", "objectivec"));
            DefaultLanguages.Add(new Language("Pascal", "pascal"));
            DefaultLanguages.Add(new Language("Perl", "perl"));
            DefaultLanguages.Add(new Language("PHP", "php"));
            DefaultLanguages.Add(new Language("PowerShell", "powershell"));
            DefaultLanguages.Add(new Language("Python", "python"));
            DefaultLanguages.Add(new Language("R", "r"));
            DefaultLanguages.Add(new Language("Ruby", "ruby"));
            DefaultLanguages.Add(new Language("Sass/Scss", "scss"));
            DefaultLanguages.Add(new Language("Scala", "scala"));
            DefaultLanguages.Add(new Language("SQL", "sql"));
            DefaultLanguages.Add(new Language("Swift", "swift"));
            DefaultLanguages.Add(new Language("TypeScript", "typescript"));
            DefaultLanguages.Add(new Language("XML", "xml"));
            DefaultLanguages.Add(new Language("XAML", "xaml"));
            DefaultLanguages.Add(new Language("YAML", "yaml"));
        }

        public static Language FromString(string str)
        {
            if (str == null)
                return null;

            foreach (Language language in DefaultLanguages)
            {
                if (string.Equals(language.Value, str, StringComparison.OrdinalIgnoreCase))
                    return language;
            }

            foreach (Language language in DefaultLanguages)
            {
                if (string.Equals(language.DisplayName, str, StringComparison.OrdinalIgnoreCase))
                    return language;
            }

            foreach (Language language in DefaultLanguages)
            {
                if (string.Equals(language.ToString(), str, StringComparison.OrdinalIgnoreCase))
                    return language;
            }

            return null;
        }

        public static string GetValueFromString(string str)
        {
            if (str == null)
                return null;

            Language language = FromString(str);
            if (language != null)
                return language.Value;

            return str;
        }

        public Language(string displayName, string value) : this(displayName, value, true)
        {
        }

        public Language(string displayName, string value, bool htmlEncode)
        {
            DisplayName = displayName;
            Value = value;
            HtmlEncode = htmlEncode;
        }

        public string DisplayName { get; }
        public string Value { get; }
        public bool HtmlEncode { get; }

        public override string ToString()
        {
            return $"{DisplayName}";
        }
    }
}