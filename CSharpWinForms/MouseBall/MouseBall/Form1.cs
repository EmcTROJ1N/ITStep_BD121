using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseBall
{
    public partial class Form1 : Form
    {
        // Конечные координаты движения шарика
        double x2, y2;

        // Текущие координаты шарика
        double currentX, currentY;
        
        // Приращение координат шарика (может быть отрицательным)
        // Расстояние, которое пройдёт шарик за 1 шаг
        double deltaX, deltaY;

        PictureBox pictureBox1 = new PictureBox();
        List<Ball> Balls;

        public Form1()
        {
            InitializeComponent();
            Balls = new List<Ball>();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

            Balls.Add(new Ball(e.X, e.Y));
            Balls.LastOrDefault().Parent = this;
            Balls.LastOrDefault().Click += Ball_Click;
            Balls.LastOrDefault().Move(0, 0);

            //timer1.Start();

            
        }

        private void Ball_Click(object sender, EventArgs e)
        {
            Controls.Remove(Balls.LastOrDefault());
            Balls.Remove(Balls.LastOrDefault());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}
