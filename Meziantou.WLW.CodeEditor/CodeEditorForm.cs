using System.Linq;
using System.Windows.Forms;

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
    }
}
