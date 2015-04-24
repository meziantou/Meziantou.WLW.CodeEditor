using System;
using System.Linq;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace Meziantou.WLW.CodeEditor
{
    public partial class CodeEditorForm : Form
    {
        public CodeEditorForm()
        {
            InitializeComponent();
            comboBoxLanguage.DataSource = CodeEditor.Language.DefaultLanguages;
        }

        public string Language
        {
            get { return comboBoxLanguage.Text; }
            set { comboBoxLanguage.Text = value; }
        }

        public string Code
        {
            get { return textBoxCode.Text; }
            set { textBoxCode.Text = value; }
        }

        public string LineHighlight
        {
            get { return textBoxLines.Text; }
            set { textBoxLines.Text = value; }
        }

        public void UpdateSmartContent(ISmartContent smartContent)
        {
            if (smartContent == null) throw new ArgumentNullException(nameof(smartContent));

            smartContent.SetCode(Code);
            smartContent.SetLanguage(Language);
            smartContent.SetLineHighlight(LineHighlight);
        }


        public void InitFromSmartContent(ISmartContent smartContent)
        {
            if (smartContent == null)
                return;

            Code = smartContent.GetCode();
            Language = smartContent.GetLanguage();
            LineHighlight = smartContent.GetLineHighlight();
        }
    }
}
