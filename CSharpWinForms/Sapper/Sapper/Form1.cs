using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sapper
{
    public partial class Form1 : Form
    {
        Box[][] Field;
        int MarkedBombCount = 0;
        int BombCount = 0;
        public Form1()
        {
            InitializeComponent();

            Random random = new Random();
            Field = new Box[15][];

            for (int i = 0; i < Field.Length; i++)
            {
                Field[i] = new Box[10];
                for (int j = 0; j < Field[i].Length; j++)
                {
                    int k = 50;
                    Field[i][j] = new Box(random, i, j);
                    Field[i][j].Size = new Size(k, k);
                    Field[i][j].Location = new Point(i * k , j * k + 5 * k);
                    Field[i][j].Parent = this;
                    Field[i][j].MouseUp += Button_Click;
                    Field[i][j].Text = "";
                    Field[i][j].BackColor = Color.White;
                    BombCount += Field[i][j].IsBomb ? 1 : 0;

                }
            }
        }

        private void Button_Click(object sender, MouseEventArgs e)
        {
            Box box = (Box)sender;

            if (e.Button == MouseButtons.Right && box.Text == "")
            {
                box.Text = "Bomb";
                box.BackgroundImage = Image.FromFile("C:\\Users\\19et7\\Desktop\\Sapper\\Sapper\\Flag.png");
                box.BackgroundImageLayout = ImageLayout.Zoom;
                box.BackColor = Color.Red;

                MarkedBombCount += box.IsBomb ? 1 : 0;

                return;
            }
            
            if (e.Button == MouseButtons.Right && box.Text == "Bomb")
            {
                box.Text = "";
                box.BackColor = Color.White;
                box.BackgroundImage = null;

                MarkedBombCount -= box.IsBomb ? 0 : 1;
                return;
            }

            if (e.Button == MouseButtons.Left && box.Text == "Bomb")
                return;

            if (box.IsBomb)
            {

                for (int i = 0; i < Field.Length; i++)
                {
                    for (int j = 0; j < Field[i].Length; j++)
                    {
                        if (Field[i][j].IsBomb)
                        {
                            Field[i][j].BackgroundImage = Image.FromFile("C:\\Users\\19et7\\Desktop\\Sapper\\Sapper\\bomb.png");
                            Field[i][j].BackgroundImageLayout = ImageLayout.Zoom;
                        }

                    }
                }

                MessageBox.Show("Ты проиграл");

                this.Close();
            }

            int k = 0;

            if (Field[box.Idx.X + 1][box.Idx.Y].IsBomb == true) k++;
            if (Field[box.Idx.X + 1][box.Idx.Y + 1].IsBomb == true) k++;
            if (Field[box.Idx.X + 1][box.Idx.Y - 1].IsBomb == true) k++;
            if (Field[box.Idx.X][box.Idx.Y - 1].IsBomb == true) k++;
            if (Field[box.Idx.X - 1][box.Idx.Y - 1].IsBomb == true) k++;
            if (Field[box.Idx.X - 1][box.Idx.Y].IsBomb == true) k++;
            if (Field[box.Idx.X - 1][box.Idx.Y + 1].IsBomb == true) k++;
            if (Field[box.Idx.X][box.Idx.Y + 1].IsBomb == true) k++;
        
            Label label = new Label();
            label.Size = box.Size;
            label.Location = box.Location;
            label.Parent = this;
            label.Text = k.ToString();
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);

            box.Visible = false;
        }

        private void ChangeNameButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.ShowDialog();
        }
    }
}
