using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
using Xceed.Wpf.Toolkit;

namespace WPF_MovingObjects
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public enum States { Move, Resize, Rotate, Remove }
        public enum Figure { Rectangle, Polygon, Ellipse, Line, Path }

        public MainWindow()
        {
            InitializeComponent();
        }


        // Ссылка на передвигаемый объект
        FrameworkElement CurrentElement = null;

        double Scale = 1.0;
        double Rotate = 0;

        // Координаты нажатия в передвигаемом объекте
        Point elementCoords;
        States State = States.Move;
        Figure _Figure = Figure.Rectangle;

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
                            Scale -= .001;
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
                case (States.Remove):
                    FieldCanvas.Children.Remove(CurrentElement);
                    break;

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

        private void MenuItem_Move(object sender, RoutedEventArgs e) => State = States.Move;
        private void MenuItem_Rotate(object sender, RoutedEventArgs e) => State = States.Rotate;
        private void MenuItem_Resize(object sender, RoutedEventArgs e) => State = States.Resize;
        private void MenuItem_Remove(object sender, RoutedEventArgs e) => State = States.Remove;

        private void MenuItem_Rect(object sender, RoutedEventArgs e) => _Figure = Figure.Rectangle;
        private void MenuItem_Polygon(object sender, RoutedEventArgs e) => _Figure = Figure.Polygon;
        private void MenuItem_Besye(object sender, RoutedEventArgs e) => _Figure = Figure.Path;
        private void MenuItem_Ellipse(object sender, RoutedEventArgs e) => _Figure = Figure.Ellipse;
        private void MenuItem_Line(object sender, RoutedEventArgs e) => _Figure = Figure.Line;

        private void FieldCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush((Color)Picker.SelectedColor);
            switch (_Figure)
            {
                case Figure.Rectangle:
                    {
                        Rectangle rect = new Rectangle();
                        rect.Width = 200;
                        rect.Height = 100;
                        Point pos = e.GetPosition(this);
                        Canvas.SetLeft(rect, pos.X);
                        Canvas.SetTop(rect, pos.Y);
                        rect.MouseDown += Element_MouseDown;
                        rect.Stroke = brush;
                        rect.Fill = brush;
                        rect.RenderTransformOrigin = new Point(0.5, 0.5);
                        FieldCanvas.Children.Add(rect);
                        break;
                    }

                case Figure.Line:
                    {
                        Line line = new Line();
                        Point pos = e.GetPosition(this);
                        line.MouseDown += Element_MouseDown;
                        line.Stroke = brush;
                        line.StrokeThickness = 10;
                        line.RenderTransformOrigin = new Point(0.5, 0.5);

                        line.X1 = pos.X;
                        line.Y1 = pos.Y;
                        line.X2 = pos.X + 200;
                        line.Y2 = pos.Y + 200;

                        FieldCanvas.Children.Add(line);
                        break;
                    }

                case Figure.Ellipse:
                    {
                        Ellipse ellipse = new Ellipse();
                        ellipse.Width = 200;
                        ellipse.Height = 100;

                        Point pos = e.GetPosition(this);
                        Canvas.SetLeft(ellipse, pos.X);
                        Canvas.SetTop(ellipse, pos.Y);

                        ellipse.RenderTransformOrigin = new Point(0.5, 0.5);
                        ellipse.MouseDown += Element_MouseDown;
                        ellipse.Stroke = brush;
                        ellipse.Fill = brush;

                        FieldCanvas.Children.Add(ellipse);
                        break;
                    }

                case Figure.Polygon:
                    {
                        Polygon myPolygon = new Polygon();
                        myPolygon.Stroke = brush;
                        myPolygon.Fill = brush;
                        myPolygon.StrokeThickness = 2;
                        myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
                        myPolygon.VerticalAlignment = VerticalAlignment.Center;


                        Point pos = e.GetPosition(this);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(new Point(pos.X, pos.Y));
                        myPointCollection.Add(new Point(pos.X + 100, pos.Y));
                        myPointCollection.Add(new Point(pos.X + 100, pos.Y + 200));
                        myPolygon.Points = myPointCollection;

                        myPolygon.MouseDown += Element_MouseDown;
                        myPolygon.RenderTransformOrigin = new Point(0.5, 0.5);
                        
                        FieldCanvas.Children.Add(myPolygon);
                        break;
                    }
                case Figure.Path:
                    {
                        Path myPath = new Path();
                        myPath.Stroke = brush;
                        myPath.StrokeThickness = 15;
                        myPath.StrokeStartLineCap = PenLineCap.Round;
                        myPath.StrokeEndLineCap = PenLineCap.Round;
                        myPath.StrokeDashCap = PenLineCap.Round;
                        myPath.MouseDown += Element_MouseDown;
                        myPath.RenderTransformOrigin = new Point(0.5, 0.5);

                        Point pos = e.GetPosition(this);
                        myPath.Data = Geometry.Parse($"M 241,200 A 20, 20 0 0 0 200, 240 C 210, 250 240, 270 240, 270 C 240, 270 260, 260 280, 240 A 20, 20 0 0 0 239, 200");
                        
                        FieldCanvas.Children.Add(myPath);
                        break;
                    }

            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (FieldCanvas == null) return;
            
            SolidColorBrush brush = new SolidColorBrush((Color)e.NewValue);
            foreach (FrameworkElement item in FieldCanvas.Children)
            {
                ((Shape)item).Stroke = brush;
                ((Shape)item).Fill = brush;
            }
        }

        private void MenuItem_Save(object sender, RoutedEventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            System.IO.FileStream fs = new System.IO.FileStream("backup.dat", System.IO.FileMode.OpenOrCreate);

            List<FigureContainer> controls = new List<FigureContainer>();
            foreach (FrameworkElement item in FieldCanvas.Children)
            {
                FigureContainer figure = new FigureContainer();
                figure.Width = item.Width;
                figure.Height = item.Height;
                figure.X = Canvas.GetLeft(item);
                figure.Y = Canvas.GetTop(item);
                
                figure.Angle = (item.RenderTransform as TransformGroup)?.Children.OfType<RotateTransform>()?.FirstOrDefault()?.Angle;
                figure.Scale = (item.RenderTransform as TransformGroup)?.Children.OfType<ScaleTransform>().FirstOrDefault().ScaleX;
                
                Color color = (Color)Picker.SelectedColor;
                figure.R = color.R;
                figure.G = color.G;
                figure.B = color.B;

                if (item is Line) figure.Type = MainWindow.Figure.Line;
                if (item is Rectangle) figure.Type = MainWindow.Figure.Rectangle;
                if (item is Ellipse) figure.Type = MainWindow.Figure.Ellipse;
                if (item is Polygon) figure.Type = MainWindow.Figure.Polygon;
                if (item is Path) figure.Type = MainWindow.Figure.Path;

                controls.Add(figure);
            }
            bf.Serialize(fs, controls);
            fs.Close();
        }

        private void MenuItem_Load(object sender, RoutedEventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            System.IO.FileStream fs = new System.IO.FileStream("backup.dat", System.IO.FileMode.OpenOrCreate);
            List<FigureContainer> controls = (List<FigureContainer>)bf.Deserialize(fs);
            fs.Close();

            FieldCanvas.Children.Clear();
            
            foreach (FigureContainer figure in controls)
            {
                FrameworkElement elem = new FrameworkElement();

                switch (figure.Type)
                {
                    case MainWindow.Figure.Line: elem = new Line(); break;
                    case MainWindow.Figure.Ellipse: elem = new Ellipse();  break;
                    case MainWindow.Figure.Rectangle: elem = new Rectangle(); break;
                    case MainWindow.Figure.Polygon: elem = new Polygon(); break;
                    case MainWindow.Figure.Path: elem = new Path(); break;
                }
                SetFigureParams(elem, figure);
                
                FieldCanvas.Children.Add(elem);

            }
        }

        void SetFigureParams(FrameworkElement figure, FigureContainer container)
        {
            figure.Width = container.Width;
            figure.Height = container.Height;
            Canvas.SetLeft(figure, container.X);
            Canvas.SetTop(figure, container.Y);

            TransformGroup transformGroup = new TransformGroup();
            RotateTransform rotateTransform = new RotateTransform();
            rotateTransform.Angle = container.Angle != null ? (double)container.Angle : 0;
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = container.Scale != null ? (double)container.Scale : 1;
            scaleTransform.ScaleY = scaleTransform.ScaleX;
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(scaleTransform);
            figure.RenderTransform = transformGroup;

            figure.RenderTransformOrigin = new Point(0.5, 0.5);
            figure.MouseDown += Element_MouseDown;

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(container.R, container.G, container.B));
            ((Shape)figure).Fill = brush;
            ((Shape)figure).Stroke = brush;
            Picker.SelectedColor = Color.FromRgb(container.R, container.G, container.B);
        }

    }
}
