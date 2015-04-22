using System;
using System.Linq;
using System.Windows.Forms;
using WindowsLive.Writer.Api;

namespace Meziantou.WLW.CodeEditor
{
    public partial class CodeSmartContentEditor : SmartContentEditor
    {
        private readonly ISmartContentEditorSite _editorSite;

        public CodeSmartContentEditor(ISmartContentEditorSite editorSite)
        {
            if (editorSite == null) throw new ArgumentNullException(nameof(editorSite));
            _editorSite = editorSite;
            InitializeComponent();
            comboBoxLanguage.DataSource = Language.DefaultLanguages;
            
            SelectedContentChanged += CodeSmartContentEditor_SelectedContentChanged;
        }

        private void CodeSmartContentEditor_SelectedContentChanged(object sender, EventArgs e)
        {
            if (SelectedContent == null)
                return;

            comboBoxLanguage.Text = SelectedContent.GetLanguage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedContent == null)
                return;

            CodeEditorForm form = new CodeEditorForm();
            form.Language = SelectedContent.GetLanguage();
            form.Code = SelectedContent.GetCode();
            var result = form.ShowDialog(ParentForm);
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                SelectedContent.SetCode(form.Code);
                SelectedContent.SetLanguage(form.Language);
                OnContentEdited();
            }
        }

        private void comboBoxLanguage_TextChanged(object sender, EventArgs e)
        {
            if (SelectedContent == null)
                return;

            SelectedContent.SetLanguage(comboBoxLanguage.Text);
            OnContentEdited();
        }
    }
}
