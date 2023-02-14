using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_MovingObjects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public enum States { Move, Resize, Rotate }
        int IdxImgGridX = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        // Ссылка на передвигаемый объект
        FrameworkElement CurrentElement = null;
        //ScaleTransform Scale = new ScaleTransform();
        //RotateTransform Rotate = new RotateTransform();
        //TransformGroup Group = new TransformGroup();

        double Scale = 1.0;
        double Rotate = 0;

        // Координаты нажатия в передвигаемом объекте
        Point elementCoords;
        States State = States.Move;

        private void Element_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CurrentElement = (FrameworkElement)sender;

            // Получить координаты мыши внутри перемещаемого объекта
            elementCoords = e.GetPosition(CurrentElement);

            // Поместить выбранный объект на самый верхний Z-уровень
            Canvas.SetZIndex(CurrentElement, 10);

            // Наложить эффект тени
            CurrentElement.Effect = new DropShadowEffect
            {
                // Цвет тени
                Color = new Color { A = 0, R = 0, G = 0, B = 0 },

                // Угол тени
                Direction = 300,

                // Радиус (величина тени)
                BlurRadius = 50,

                // Качество отрисовки
                RenderingBias = RenderingBias.Quality,

                // Дистанция от объекта
                ShadowDepth = 10,

                // Прозрачность тени
                Opacity = 0.8
            };

            // отметить сообщение, как обработанное
            e.Handled = true;
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentElement == null)
                return;

            Point coords = e.GetPosition(FieldCanvas);
            
            switch (State)
            {
                case (States.Move):
                {
                    // Перемещение элемента по новым координатам мыши, с учётом места нажатия на элементе
                    Canvas.SetLeft(CurrentElement, coords.X - elementCoords.X);
                    Canvas.SetTop(CurrentElement, coords.Y - elementCoords.Y);
                    break;
                }

                case (States.Resize):
                    {
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            Scale += .01;
                            scaleTransform.ScaleX = Scale;
                            scaleTransform.ScaleY = Scale;
                            
                            RotateTransform rotateTransform = new RotateTransform();
                            rotateTransform.Angle = Rotate;

                            TransformGroup groupTransform = new TransformGroup();
                            groupTransform.Children.Add(scaleTransform);
                            groupTransform.Children.Add(rotateTransform);

                            CurrentElement.RenderTransform = groupTransform;

                        }
                        if (e.RightButton == MouseButtonState.Pressed)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            Scale -= .01;
                            scaleTransform.ScaleX = Scale;
                            scaleTransform.ScaleY = Scale;
                            
                            RotateTransform rotateTransform = new RotateTransform();
                            rotateTransform.Angle = Rotate;

                            TransformGroup groupTransform = new TransformGroup();
                            groupTransform.Children.Add(scaleTransform);
                            groupTransform.Children.Add(rotateTransform);

                            CurrentElement.RenderTransform = groupTransform;

                        }

                        break;
                    }

                case (States.Rotate):
                    {
                        if (e.LeftButton == MouseButtonState.Pressed)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            scaleTransform.ScaleX = Scale;
                            scaleTransform.ScaleY = Scale;
                            
                            RotateTransform rotateTransform = new RotateTransform();
                            Rotate -= .1;
                            rotateTransform.Angle = Rotate;

                            TransformGroup groupTransform = new TransformGroup();
                            groupTransform.Children.Add(scaleTransform);
                            groupTransform.Children.Add(rotateTransform);

                            CurrentElement.RenderTransform = groupTransform;

                        }
                        if (e.RightButton == MouseButtonState.Pressed)
                        {
                            ScaleTransform scaleTransform = new ScaleTransform();
                            scaleTransform.ScaleX = Scale;
                            scaleTransform.ScaleY = Scale;
                            
                            RotateTransform rotateTransform = new RotateTransform();
                            Rotate += .1;
                            rotateTransform.Angle = Rotate;

                            TransformGroup groupTransform = new TransformGroup();
                            groupTransform.Children.Add(scaleTransform);
                            groupTransform.Children.Add(rotateTransform);

                            CurrentElement.RenderTransform = groupTransform;
                        }

                        break;
                    }
            }
        }

        private void mainCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (CurrentElement != null)
            {
                // Отменить эффект тени
                CurrentElement.ClearValue(EffectProperty);

                // Поместить отпущенный объект на самый нижний Z-уровень
                Canvas.SetZIndex(CurrentElement, 0);

                CurrentElement = null;
            }
        }

        private void Field_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffects & DragDropEffects.Copy) != 0)
                e.Effects = DragDropEffects.Copy;
        }

        private void Field_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] arr = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string str in arr)
                {
                    FileInfo file = new FileInfo(str);

                    if (new string[] { ".jpg", ".bmp", ".png" }.Contains(file.Extension))
                    {
                        Image img = new Image();

                        img.Width = 200;
                        img.Height = 100;
                        img.Source = new BitmapImage(new Uri(file.FullName));
                        img.RenderTransformOrigin = new Point(0.5, 0.5);
                        
                        Point coords = e.GetPosition(FieldCanvas);
                        Canvas.SetLeft(img, coords.X);
                        Canvas.SetTop(img, coords.Y);
                        Canvas.SetZIndex(img, 0);
                        
                        img.MouseDown += Element_MouseDown;

                        FieldCanvas.Children.Add(img);
                        
                        
                        Image img2 = new Image();
                        img2.Source = new BitmapImage(new Uri(file.FullName));
                        img2.Width = 100;
                        img2.Height = 200;

                        FieldGrid.Children.Add(img2);
                        Grid.SetColumn(img2, IdxImgGridX);
                        Grid.SetRow(img2, 0);
                        
                        if (++IdxImgGridX == FieldGrid.ColumnDefinitions.Count)
                            FieldGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                }
            }
        }

        private void MenuItem_Move(object sender, RoutedEventArgs e) => State = States.Move;
        private void MenuItem_Rotate(object sender, RoutedEventArgs e) => State = States.Rotate;
        private void MenuItem_Resize(object sender, RoutedEventArgs e) => State = States.Resize;
    }
}
