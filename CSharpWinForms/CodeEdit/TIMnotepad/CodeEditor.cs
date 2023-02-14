using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO.Compression;
using CodeEditorNamespace;

namespace CodeEditor
{
    public partial class CodeEditor : Form
    {
        public CodeEditor()
        {
            InitializeComponent();
            Tabs.TabPages[0].Controls.Add(new CodeEditPage());
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            
            foreach (Control control in Tabs.SelectedTab.Controls[0].Controls)
            {
                if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    File.WriteAllText(saveFileDialog1.FileName, ((FastColoredTextBoxNS.FastColoredTextBox)control).Text);
            }
            

        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSharp файлы исходного кода (*.cs)|*.cs|Tim Notepad File (*.tnf)|*.tnf";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string text = File.ReadAllText(openFileDialog1.FileName);

            foreach (Control control in Tabs.SelectedTab.Controls[0].Controls)
            {
                if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    ((FastColoredTextBoxNS.FastColoredTextBox)control).Text = text;
            }
            Tabs.SelectedTab.Text = openFileDialog1.FileName;


        }

        private void FontSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            foreach (Control control in Tabs.SelectedTab.Controls[0].Controls)
            {
                if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    ((FastColoredTextBoxNS.FastColoredTextBox)control).Font = fontDialog1.Font;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void mode2_Resize(object sender, EventArgs e)
        {
            panel3.Location = new Point((Width / 2) - (537 / 2), (Height / 2) - (424 / 2));
            panel2.Location = new Point((Width / 2) - (533 / 2), (Height / 2) - (483 / 2));
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPage = new TabPage();
            tabPage.Text = "New doc";
            Tabs.TabPages.Add(tabPage);
            Tabs.TabPages[Tabs.TabPages.IndexOf(tabPage)].Controls.Add(new CodeEditPage());
        }
    }
}
