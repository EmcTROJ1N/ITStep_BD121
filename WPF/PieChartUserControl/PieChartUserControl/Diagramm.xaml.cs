using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PieChartUserControl
{
    /// <summary>
    /// Логика взаимодействия для Diagramm.xaml
    /// </summary>
    public partial class Diagramm : UserControl
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(double[]), typeof(Diagramm), new PropertyMetadata(null, OnDataChanged));

        public double[] Data
        {
            get { return (double[])GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        Random _Random = new Random();

        public Diagramm()
        {
            InitializeComponent();
        }

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Diagramm)d;
            control.Draw();
        }

        private void Draw()
        {
            double total = Data.Sum();
            double startAngle = 0;

            chartCanvas.Children.Clear();

            for (int i = 0; i < Data.Length; i++)
            {
                double angle = 360 * Data[i] / total;
                double endAngle = startAngle + angle;

                Path sector = new Path
                {
                    Fill = new SolidColorBrush(GetRandomColor()),
                    Data = new PathGeometry(
                        new PathFigure[]
                        {
                    new PathFigure(
                        new Point(250, 250),
                        new PathSegment[]
                        {
                            new LineSegment(
                                new Point(250 + 200 * Math.Cos(startAngle * Math.PI / 180),
                                          250 * 2 - 200 * Math.Sin(startAngle * Math.PI / 180)),
                                true
                            ),
                            new ArcSegment(
                                new Point(250 + 200 * Math.Cos(endAngle * Math.PI / 180),
                                          250 * 2 - 200 * Math.Sin(endAngle * Math.PI / 180)),
                                new Size(200, 200),
                                0,
                                false,
                                SweepDirection.Clockwise,
                                true
                            )
                        },
                        true
                    )
                        }
                    )
                };

                chartCanvas.Children.Add(sector);

                startAngle = endAngle;
            }
        }

        private Color GetRandomColor() =>
            Color.FromRgb((byte)_Random.Next(256), (byte)_Random.Next(256), (byte)_Random.Next(256));
    }
}
