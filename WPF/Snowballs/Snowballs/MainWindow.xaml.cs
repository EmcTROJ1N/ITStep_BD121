using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snowballs
{

    public partial class MainWindow : Window
    {
        int Health = 100;
        int CountShots = 0;
        int CountEnemies = 3;
        List<Enemy> Enemies = new List<Enemy>();
        List<Uri> SpriteUri = new List<Uri>();

        Random _Random = new Random();

        int MinSnow = 5;
        int MaxSnow = 25;

        double heel = 0;

        DispatcherTimer ShootingTimer = new DispatcherTimer();
        DispatcherTimer SnowingTimer = new DispatcherTimer();
        MediaPlayer Player = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            SpriteUri.Add(new Uri("/Resource/Enemy.png", UriKind.Relative));
            SpriteUri.Add(new Uri("/Resource/Enemy2.png", UriKind.Relative));
            SpriteUri.Add(new Uri("/Resource/Enemy3.png", UriKind.Relative));
            Player.Open(new Uri("sound.mp3", UriKind.Relative));
            Player.Volume = 1;
            Player.Balance = 0;
            Player.Position = new TimeSpan(0, 0, 0);
            Player.SpeedRatio = 1;
            Player.Play();

            HealthLabel.Content = $"Health: {Health}";
            ShotsLabel.Content = $"Count taps: {CountShots}";

            ShootingTimer.Tick += ShootingTimer_Tick;
            ShootingTimer.Interval = TimeSpan.FromSeconds(.5);
            ShootingTimer.Start();

            SnowingTimer.Tick += SnowingTimer_Tick;
            SnowingTimer.Interval = TimeSpan.FromSeconds(.1);
            SnowingTimer.Start();

            HealthBar.Maximum = 100;
            HealthBar.Minimum = 0;
            HealthBar.Value = 100;
        }

        private void SnowingTimer_Tick(object sender, EventArgs e)
        {
           
            Ellipse snowflake = new Ellipse();

            snowflake.Width = _Random.Next(MinSnow,MaxSnow);
            snowflake.Height = snowflake.Width;

            Canvas.SetLeft(snowflake, _Random.Next(0, (int)Field.ActualWidth));
            Canvas.SetTop(snowflake, -10);
            snowflake.Fill = new RadialGradientBrush(Color.FromRgb(255, 255, 255), Color.FromArgb(0, 255, 255, 255));
            Field.Children.Add(snowflake);

            DoubleAnimation snowfall = new DoubleAnimation();

            snowfall.Duration = TimeSpan.FromSeconds(2);
            snowfall.From = Canvas.GetTop(snowflake);
            snowfall.To = Field.ActualHeight - snowflake.Height;

            snowflake.BeginAnimation(Canvas.TopProperty, snowfall);

        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Point coords = e.GetPosition(Field);
            Ellipse snowball = new Ellipse();
            snowball.Tag = "snowball";

            snowball.Width = 100;
            snowball.Height = snowball.Width;

            Canvas.SetLeft(snowball, Field.ActualWidth / 2);
            Canvas.SetTop(snowball, Field.ActualHeight);

            RadialGradientBrush brush = new RadialGradientBrush(Color.FromRgb(255, 255, 255), Color.FromArgb(0, 255, 255, 255));

            brush.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 255), 0.7));
            brush.GradientStops.Add(new GradientStop(Color.FromRgb(160, 255, 255), 0.8));

            snowball.Fill = brush;

            Field.Children.Add(snowball);

            DoubleAnimation moveLeft = new DoubleAnimation();
            DoubleAnimation moveTop = new DoubleAnimation();

            DoubleAnimation scaleAnimation = new DoubleAnimation(1, .3, TimeSpan.FromSeconds(0.5));
            ScaleTransform trans = new ScaleTransform();
            snowball.RenderTransform = trans;

            moveLeft.From = Canvas.GetLeft(snowball);
            moveLeft.To = coords.X;
            moveLeft.Duration = TimeSpan.FromSeconds(.5);

            moveTop.From = Canvas.GetTop(snowball);
            moveTop.To = coords.Y;
            moveLeft.Duration = TimeSpan.FromSeconds(.5);

            moveTop.Completed += (sender, eArgs) =>
            {
                foreach (FrameworkElement elem in Field.Children)
                {
                   if ((string)(elem.Tag) == "snowball")
                   {
                      Field.Children.Remove(elem);
                      break;
                   }
                }
            };

            trans.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            snowball.BeginAnimation(Canvas.TopProperty, moveTop);
            snowball.BeginAnimation(Canvas.LeftProperty, moveLeft);
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < CountEnemies; i++)
            {
                Enemies.Add(new Enemy(new BitmapImage(SpriteUri[_Random.Next(0, SpriteUri.Count)]), this));
                Enemies[i].Sprite.MouseDown += (sender, e) => { ShotsLabel.Content = $"Count taps: {++CountShots}"; e.Handled = true; };
            }
        }

        private void ShootingTimer_Tick(object sender, EventArgs e)
        {
            DoubleAnimation shootingHealthBar;
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].Shoot() == false)
                {
                    if (Health < 100)
                        heel += 0.05;
                    if (heel>=1)
                    {

                        heel = 0;
                        shootingHealthBar = new DoubleAnimation();
                        shootingHealthBar.Duration = TimeSpan.FromSeconds(.5);
                        shootingHealthBar.From = Health;
                        shootingHealthBar.To = Health +1;
                        HealthBar.BeginAnimation(ProgressBar.ValueProperty, shootingHealthBar);
                        Health++;
                        HealthLabel.Content = $"Health: {Health}";
                    }
                    continue; 
                } 

                shootingHealthBar = new DoubleAnimation();
                shootingHealthBar.Duration = TimeSpan.FromSeconds(.5);
                shootingHealthBar.From = Health;
                shootingHealthBar.To = Health - 10;

                DoubleAnimation shakeWind = new DoubleAnimation(0, -20, TimeSpan.FromSeconds(.5));
                shakeWind.AutoReverse = true;

                Background.BeginAnimation(Canvas.LeftProperty, shakeWind);

                HealthBar.BeginAnimation(ProgressBar.ValueProperty, shootingHealthBar);
                Health -= 10;
                HealthLabel.Content = $"Health: {Health}";

                if (Health <= 0)
                {
                    MessageBox.Show("Game over");
                    this.Close();
                }
            }

        }

    }
}
