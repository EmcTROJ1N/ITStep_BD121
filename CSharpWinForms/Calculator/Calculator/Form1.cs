using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            foreach (Control control in Controls)
            {
                Button button = control as Button;
                if (button != null) button.Click += Button_Click;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string currentButtonText = ((Button)sender).Text;
            
            switch (currentButtonText)
            {
                case "C": Label.Text = ""; break;
                case "CE": Label.Text = ""; break;
                case "Back": Label.Text = Label.Text.Substring(0, Label.Text.Length - 1); break;
                case "=": Label.Text = new DataTable().Compute(Label.Text, null).ToString(); break;
                case "+-":
                    {
                        if (Int32.TryParse(Label.Text, out var num))
                            Label.Text = (-num).ToString();
                        break;
                    }
                case "pow 2":
                    {
                        if (Int32.TryParse(Label.Text, out var num))
                            Label.Text = Math.Pow(num, 2).ToString();
                        break;
                    }
                case "1/x":
                    {
                        if (Int32.TryParse(Label.Text, out var num))
                            Label.Text = (1 / num).ToString();
                        break;
                    }
                case "sqrt": break;
                    {
                        if (Int32.TryParse(Label.Text, out var num))
                            Label.Text = Math.Sqrt(num).ToString();
                        break;
                    }


                default: Label.Text += currentButtonText; break;
            }
        }
    }
}
