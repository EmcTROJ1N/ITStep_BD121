using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snowballs
{
    public class Enemy
    {
        public Image Sprite = new Image();
        MainWindow Root;
        DispatcherTimer CoolDownTimer = new DispatcherTimer();
        bool IsReadyShooting = false;
        RadialGradientBrush FillSnowball = new RadialGradientBrush(Color.FromRgb(255, 255, 255), Color.FromArgb(0, 255, 255, 255));

        int CoolDown;
        Canvas Parent;
        int Size = 100;
        bool ShootNow = false;
        int mn = 60;

        public RadialGradientBrush BrushSnow => FillSnowball;

        Random _Random = new Random();

        public Enemy(BitmapImage bmp, MainWindow parent)
        {
            CoolDown = _Random.Next(-6, 1);

            Sprite.Source = bmp;
            Sprite.Width = _Random.Next(50, 300);
            Sprite.Height = Sprite.Width * 2;
            Sprite.MouseDown += Sprite_MouseDown;

            Canvas.SetLeft(Sprite, _Random.Next(0, (int)parent.ActualWidth - (int)Sprite.Width));
            Canvas.SetTop(Sprite, _Random.Next(0, (int)parent.ActualHeight - (int)Sprite.Height));

            Parent = parent.Field;
            Parent.Children.Add(Sprite);
            Root = parent;


            FillSnowball.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.7));
            FillSnowball.GradientStops.Add(new GradientStop(Color.FromRgb(160, 255, 255), 0.8));


            CoolDownTimer.Interval = TimeSpan.FromSeconds(1);
            CoolDownTimer.Tick += CoolDownTimer_Tick;

            CoolDownTimer.Start();

        }

        private void Sprite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point coords = e.GetPosition(Parent);

            ShootNow = true;

            Ellipse snowball = new Ellipse();
            snowball.Tag = "snowball";

            snowball.Width = Size;
            snowball.Height = Size;

            Canvas.SetLeft(snowball, Parent.ActualWidth / 2);
            Canvas.SetTop(snowball, Parent.ActualHeight);

            snowball.Fill = FillSnowball;

            Parent.Children.Add(snowball);

            DoubleAnimation moveLeft = new DoubleAnimation();
            DoubleAnimation moveTop = new DoubleAnimation();

            DoubleAnimation scaleAnimation = new DoubleAnimation(1, 1 - (1 / Sprite.Width * mn), TimeSpan.FromSeconds(0.5));
            ScaleTransform trans = new ScaleTransform();
            snowball.RenderTransform = trans;

            moveLeft.From = Canvas.GetLeft(snowball);
            // moveLeft.To = Canvas.GetLeft(Sprite) + Sprite.Width / 2 - Size / 2;
            moveLeft.To = coords.X;
            moveLeft.Duration = TimeSpan.FromSeconds(.5);

            moveTop.From = Canvas.GetTop(snowball);
            //moveTop.To = Canvas.GetTop(Sprite) + Sprite.Height / 2 - Size / 2;
            moveTop.To = coords.Y;

            moveLeft.Duration = TimeSpan.FromSeconds(.5);

            moveTop.Completed += (sender, eArgs) => ReSpawn();

            trans.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            snowball.BeginAnimation(Canvas.TopProperty, moveTop);
            snowball.BeginAnimation(Canvas.LeftProperty, moveLeft);

            CoolDown = 0;
            //e.Handled = true;
        }

        public void ReSpawn()
        {
            foreach (FrameworkElement elem in Parent.Children)
            {
                if ((string)(elem.Tag) == "snowball")
                {
                    Parent.Children.Remove(elem);
                    break;
                }
            }

            DoubleAnimation opacityAnimToZero = new DoubleAnimation();
            opacityAnimToZero.Duration = TimeSpan.FromSeconds(.5);
            opacityAnimToZero.From = 1;
            opacityAnimToZero.To = 0;
            opacityAnimToZero.Completed += OpacityAnimToZero_Completed;
            Sprite.BeginAnimation(Image.OpacityProperty, opacityAnimToZero);

            CoolDownTimer.Start();
        }

        private void OpacityAnimToZero_Completed(object sender, EventArgs e)
        {
            Random _Random = new Random();

            Canvas.SetLeft(Sprite, _Random.Next(0, (int)Parent.ActualWidth - (int)Sprite.Width));
            Canvas.SetTop(Sprite, _Random.Next(0, (int)Parent.ActualHeight - (int)Sprite.Height));

            DoubleAnimation opacityAnimToOne = new DoubleAnimation();
            opacityAnimToOne.Duration = TimeSpan.FromSeconds(.5);
            opacityAnimToOne.From = 0;
            opacityAnimToOne.To = 1;
            Sprite.BeginAnimation(Image.OpacityProperty, opacityAnimToOne);
        }

        //private void Sprite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    System.Windows.Point coords = e.GetPosition(Parent);

        //    if (ShootNow) return;
        //    ShootNow = true;

        //    Ellipse snowball = new Ellipse();
        //    snowball.Tag = "snowball";

        //    snowball.Width = Size;
        //    snowball.Height = Size;

        //    Canvas.SetLeft(snowball, Parent.ActualWidth / 2);
        //    Canvas.SetTop(snowball, Parent.ActualHeight);

        //    snowball.Fill = FillSnowball;

        //    Parent.Children.Add(snowball);

        //    DoubleAnimation moveLeft = new DoubleAnimation();
        //    DoubleAnimation moveTop = new DoubleAnimation();

        //    DoubleAnimation scaleAnimation = new DoubleAnimation(1, 1-(1/Sprite.Width*mn), TimeSpan.FromSeconds(0.5));
        //    ScaleTransform trans = new ScaleTransform();
        //    snowball.RenderTransform = trans;

        //    moveLeft.From = Canvas.GetLeft(snowball);
        //   // moveLeft.To = Canvas.GetLeft(Sprite) + Sprite.Width / 2 - Size / 2;
        //    moveLeft.To = coords.X;
        //    moveLeft.Duration = TimeSpan.FromSeconds(.5);

        //    moveTop.From = Canvas.GetTop(snowball);
        //    //moveTop.To = Canvas.GetTop(Sprite) + Sprite.Height / 2 - Size / 2;
        //    moveTop.To = coords.Y;

        //    moveLeft.Duration = TimeSpan.FromSeconds(.5);

        //    moveTop.Completed += (sender, eArgs) => ReSpawn();

        //    trans.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        //    trans.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        //    snowball.BeginAnimation(Canvas.TopProperty, moveTop);
        //    snowball.BeginAnimation(Canvas.LeftProperty, moveLeft);

        //    CoolDown = 0;
        //}
        private void CoolDownTimer_Tick(object sender, EventArgs e)
        {
            if (CoolDown == 6)
            {
                CoolDown = 0;
                IsReadyShooting = true;
                CoolDownTimer.Stop();
            }
            else CoolDown++;
        }

        public bool Shoot()
        {
            if (IsReadyShooting)
            {
                Ellipse snowball = new Ellipse();
                snowball.Width = Size;
                snowball.Height = Size;
                Canvas.SetLeft(snowball, Canvas.GetLeft(Sprite) + Sprite.Width / 2);
                Canvas.SetTop(snowball, Canvas.GetTop(Sprite) + Sprite.Height / 2);
                snowball.Fill = FillSnowball;
                Parent.Children.Add(snowball);

                double animDuration = .5;

                DoubleAnimation snowballAnimLeft = new DoubleAnimation();
                snowballAnimLeft.From = Canvas.GetLeft(snowball);
                snowballAnimLeft.To = _Random.Next((int)Parent.ActualWidth / 5, (int)Parent.ActualWidth/2+ (int)Parent.ActualWidth / 5);
                snowballAnimLeft.Duration = TimeSpan.FromSeconds(animDuration);

                DoubleAnimation snowballAnimTop = new DoubleAnimation();
                snowballAnimTop.From = Canvas.GetTop(snowball);
                snowballAnimTop.To = _Random.Next((int)Parent.ActualHeight / 5, (int)Parent.ActualHeight/2 + (int)Parent.ActualHeight / 5);
                snowballAnimTop.Duration = TimeSpan.FromSeconds(animDuration);

                DoubleAnimation snowballAnimScale = new DoubleAnimation();
                snowballAnimScale.From = snowball.Width;
                snowballAnimScale.To = snowball.Width * 10;
                snowballAnimScale.Duration = TimeSpan.FromSeconds(animDuration);

                snowballAnimScale.Completed += SnowballAnimLeft_Completed;

                snowball.BeginAnimation(Canvas.LeftProperty, snowballAnimLeft);
                snowball.BeginAnimation(Canvas.TopProperty, snowballAnimTop);
                snowball.BeginAnimation(Ellipse.WidthProperty, snowballAnimScale);
                snowball.BeginAnimation(Ellipse.HeightProperty, snowballAnimScale);

                void SnowballAnimLeft_Completed(object sender, EventArgs e)
                {
                    DoubleAnimation snowballAnimOpacity = new DoubleAnimation();
                    snowballAnimOpacity.From = 1;
                    snowballAnimOpacity.To = 0;
                    snowballAnimOpacity.Duration = TimeSpan.FromSeconds(2);
                    snowballAnimOpacity.Completed += (sender, e) => Parent.Children.Remove(snowball);

                    snowball.BeginAnimation(Ellipse.OpacityProperty, snowballAnimOpacity);
                }
                IsReadyShooting = false;
                return true;
            }
            else
                return false;
        }
    }

}
