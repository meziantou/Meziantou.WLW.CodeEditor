using System;
using System.Text;
using System.Web;
using System.Windows.Forms;
using WindowsLive.Writer.Api;
using WindowsLive.Writer.HtmlParser.Parser;

namespace Meziantou.WLW.CodeEditor
{
    [WriterPlugin("C2E9E611-067E-41C3-8482-1504673E8EED", "CodeEditor", Name = "Code (Smart)",
        Description = "Allow to edit code", HasEditableOptions = false)]
    [InsertableContentSource("Code (Smart)")]
    public class CodeEditorPlugin : SmartContentSource
    {
        public override string GeneratePublishHtml(ISmartContent content, IPublishingContext publishingContext)
        {
            string text = content.GetCode();
            string lineHightlight = content.GetLineHighlight();
            string languageName = content.GetLanguage();
            string languageValue = Language.GetValueFromString(languageName);
            if (string.IsNullOrEmpty(languageValue))
            {
                languageValue = "none";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<pre");
            if (!string.IsNullOrEmpty(lineHightlight))
            {
                sb.Append(" data-line='");
                sb.Append(lineHightlight);
                sb.Append("'");
            }
            sb.Append("><code class='language-");
            sb.Append(languageValue);
            sb.Append("'>");
            sb.Append(HttpUtility.HtmlEncode(text));
            sb.Append("</code></pre>");

            return sb.ToString();
        }

        public override SmartContentEditor CreateEditor(ISmartContentEditorSite editorSite)
        {
            return new CodeSmartContentEditor(editorSite);
        }

        public override DialogResult CreateContent(IWin32Window dialogOwner, ISmartContent newContent)
        {
            CodeEditorForm form = new CodeEditorForm();
            DialogResult result = form.ShowDialog(dialogOwner);
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                form.UpdateSmartContent(newContent);
            }
            return result;
        }
    }

    [WriterPlugin("C2E9E611-067E-41C3-8482-1504673E8EEE", "CodeEditor2", Name = "Code", Description = "Allow to edit code", HasEditableOptions = false)]
    [InsertableContentSource("Code")]
    public class CodeEditorPlugin2 : ContentSource
    {
        public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content)
        {
            string begin;
            string end;
            string languageName;
            string code;
            ExtractInfo(content, out languageName, out code, out begin, out end);
            CodeEditorForm form = new CodeEditorForm();
            form.Language = languageName ?? "";
            form.Code = code ?? "";
            DialogResult result = form.ShowDialog(dialogOwner);
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                string languageValue = Language.GetValueFromString(form.Language);
                if (string.IsNullOrEmpty(languageValue))
                {
                    languageValue = "none";
                }

                content = $"{begin}<pre><code class='language-{languageValue}'>{HttpUtility.HtmlEncode(form.Code)}</code></pre>{end}";
                return DialogResult.OK;
            }

            return DialogResult.Cancel;
        }

        static void ExtractInfo(string content, out string language, out string code, out string begin, out string end)
        {
            begin = null;
            end = null;
            language = null;
            code = content;

            if (string.IsNullOrEmpty(content))
                return;

            try
            {
                SimpleHtmlParser parser = new SimpleHtmlParser(content);
                BeginTag element = parser.Next() as BeginTag;
                if (string.Equals(element?.Name, "div", StringComparison.OrdinalIgnoreCase))
                {
                    begin = element.RawText;
                    end = "</" + element.Name + ">";
                    element = parser.Next() as BeginTag;
                }

                if (string.Equals(element?.Name, "pre", StringComparison.OrdinalIgnoreCase))
                {
                    var className = element.GetAttributeValue("class");
                    if (className != null)
                    {
                        className = className.Trim();
                        if (className.StartsWith("language-"))
                        {
                            language = className.Substring("language-".Length);
                        }
                    }

                    var codeContentTag = parser.Next() as BeginTag;
                    if (string.Equals(codeContentTag?.Name, "code", StringComparison.OrdinalIgnoreCase))
                    {
                        className = codeContentTag.GetAttributeValue("class");
                        if (className != null)
                        {
                            className = className.Trim();
                            if (className.StartsWith("language-"))
                            {
                                language = className.Substring("language-".Length);
                            }
                        }

                        int length = 0;
                        int depth = 1;
                        while (true)
                        {
                            Element elt = parser.Next();
                            if (elt != null)
                            {
                                if (elt is BeginTag)
                                    ++depth;
                                else if (elt is EndTag && --depth == 0)
                                    break;
                                length += elt.Length;
                            }
                            else
                                break;
                        }

                        string html = content.Substring(codeContentTag.Offset + codeContentTag.Length, length);
                        code = HttpUtility.HtmlDecode(html);
                    }
                }
            }
            catch
            {
            }
        }
    }


    [WriterPlugin("C2E9E611-067E-41C3-8482-1504673E8EEF", "RemoveSmartCodeEditor", Name = "Remove Smart Code", Description = "", HasEditableOptions = false)]
    [InsertableContentSource("Remove Smart Code")]
    public class RemoveSmartCodeEditorPlugin : ContentSource
    {
        public override DialogResult CreateContent(IWin32Window dialogOwner, ref string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    SimpleHtmlParser parser = new SimpleHtmlParser(content);
                    BeginTag element = parser.Next() as BeginTag;
                    if (string.Equals(element?.Name, "div", StringComparison.OrdinalIgnoreCase))
                    {
                        var preElement = parser.Peek(0) as BeginTag;
                        if (string.Equals(preElement?.Name, "pre", StringComparison.OrdinalIgnoreCase))
                        {
                            int length = 0;
                            int depth = 1;
                            while (true)
                            {
                                Element elt = parser.Next();
                                if (elt != null)
                                {
                                    if (elt is BeginTag)
                                        ++depth;
                                    else if (elt is EndTag && --depth == 0)
                                        break;
                                    length += elt.Length;
                                }
                                else
                                    break;
                            }

                            string html = content.Substring(preElement.Offset, length);
                            content = html;

                            return DialogResult.OK;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }

            return DialogResult.Cancel;
        }
    }

}
