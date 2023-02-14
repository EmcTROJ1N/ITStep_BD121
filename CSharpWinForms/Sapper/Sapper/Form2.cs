using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sapper
{
    public partial class Form2 : Form
    {
        Form1 fff;
        public Form2(Form1 form1)
        {
            InitializeComponent();
            fff = form1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            fff.welcomeLabel.Text += nameTextBox.Text;
            this.Close();
        }
    }
}
