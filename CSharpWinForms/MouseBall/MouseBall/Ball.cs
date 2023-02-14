using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseBall
{
    class Ball : System.Windows.Forms.PictureBox
    {

        System.Drawing.Point deltaPoint = new System.Drawing.Point();
        System.Drawing.Point resPoint = new System.Drawing.Point();
        System.Windows.Forms.Timer _Timer;

        public Ball(int x = 0, int y = 0)
        {
            _Timer = new System.Windows.Forms.Timer();
            _Timer.Interval = 10;
            _Timer.Tick += _Timer_Tick;

            Image = MouseBall.Properties.Resources.Ball;
            Location = new System.Drawing.Point(x, y);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "Ball";
       
            Size = new System.Drawing.Size(72, 65);
            TabIndex = 0;
            TabStop = false;
        }

        private void _Timer_Tick(object sender, EventArgs e)
        {
            // Получение новых координат шарика
            // Изменение X-координаты шарика
            Left += deltaPoint.X;
            Top += deltaPoint.Y;

            // Если шарик находится максимально близко от финальной точки - остановить движение шарика
            // Math.Abs(currentY - y2) - расстояние от текущей Y-координаты шарика до Y-финальной точки
            // Math.Abs(currentX - x2) - расстояние от текущей X-координаты шарика до X-финальной точки

            //if (currentY == y2 && currentX == x2)
            if (Math.Abs(Left - resPoint.X) < 1 && Math.Abs(Top - resPoint.Y) < 1)
                _Timer.Stop();

            // Переместить шарик в новые координаты
            //pictureBox1.Top = (int)currentY;
            //pictureBox1.Left = (int)currentX;

        }

        public void Move(int x, int y)
        {
            _Timer.Stop();

            resPoint.X = x;
            resPoint.Y = y;

            // Занести начальные координаты шарика в текущие координаты
            //currentX = pictureBox1.Left;
            //currentY = pictureBox1.Top;

            // Вычислить приращение координат шарика за 1 шаг таймера (при общих 100 шагах)
            deltaPoint.X = (x - Left) / 100;
            deltaPoint.Y = (y - Top) / 100;

            // Запуск таймера
            _Timer.Start();
        }

    }
}
