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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Spots
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Label[][] Labels;
        int StepsCount = 0;
        int Seconds = 0;
        DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

            int countCols = 4;

            for (int i = 0; i < countCols; i++)
            {
                field.ColumnDefinitions.Add(new ColumnDefinition());
                field.RowDefinitions.Add(new RowDefinition());
            }

            int[] nums = new int[field.ColumnDefinitions.Count * field.RowDefinitions.Count];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = i;

            Labels = new Label[field.ColumnDefinitions.Count][];
            for (int i = 0; i < field.RowDefinitions.Count; i++)
                Labels[i] = new Label[field.RowDefinitions.Count];


            Random random = new Random();
            for (int i = nums.Length - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = nums[j];
                nums[j] = nums[i];
                nums[i] = temp;
            }

            while (true)
            {
                int x = 0;
                for (int i = 0; i < nums.Length - 1; i++)
                {
                    if (nums[i + 1] != 0 && nums[i] != 0)
                        x += (nums[i] > nums[i + 1]) ? 1 : 0;
                }
                if (x % 2 == 0)
                    break;

                for (int i = 0; i < nums.Length - 1; i++)
                {
                    if (nums[i] > nums[i + 1])
                    {
                        int tmp = nums[i];
                       nums[i] = nums[i + 1];
                       nums[i + 1] = tmp;
                       break;
                    }
                }
            }

            for (int i = 0, k = 0; i < field.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < field.RowDefinitions.Count; j++, k++)
                {
                    Labels[i][j] = new Label();

                    if (nums[k] != 0)
                        Labels[i][j].Content = nums[k];
                    Labels[i][j].FontSize = 30;
                    Labels[i][j].FontFamily = new FontFamily("Bold");
                    Labels[i][j].HorizontalAlignment = HorizontalAlignment.Center;
                    Labels[i][j].VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(Labels[i][j], i);
                    Grid.SetRow(Labels[i][j], j);

                    field.Children.Add(Labels[i][j]);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e) =>
            TimeTextBox.Text = $"Времени прошло: {++Seconds}";

        private void field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int cN = 0;
            int rN = 0;

            // получить координаты мыши в Grid
            Point mousePos = e.GetPosition(field);

            //MessageBox.Show($"Coords: {mousePos}");

            // вычисление номера столбца, по которому пришёлся щелчок
            double width = 0;
            for (int i = 0; i < field.ColumnDefinitions.Count; i++)
            {
                // прибавсть к общему размеру размер текущего столбца
                width += field.ColumnDefinitions[i].ActualWidth;
                if (width > mousePos.X)
                {
                    cN = i;
                    break;
                }
            }

            // вычисление номера строки, по которому пришёлся щелчок
            double height = 0;
            for (int i = 0; i < field.RowDefinitions.Count; i++)
            {
                height += field.RowDefinitions[i].ActualHeight;
                if (height > mousePos.Y)
                {
                    rN = i;
                    break;
                }
            }

            int tmp1 = cN;
            cN = rN;
            rN = tmp1;

            if (cN + 1 >= 0 && (cN + 1) < field.ColumnDefinitions.Count && Labels[rN][cN + 1].Content == null)
                {
                    object tmp = Labels[rN][cN].Content;
                    Labels[rN][cN].Content = Labels[rN][cN + 1].Content;
                    Labels[rN][cN + 1].Content = tmp;
                    StepsTextBox.Text = $"Шагов сделано: {++StepsCount}";
                }

             if (rN + 1 >= 0 && (rN + 1) < field.ColumnDefinitions.Count && Labels[rN + 1][cN].Content == null)
             {
                    object tmp = Labels[rN][cN].Content;
                    Labels[rN][cN].Content = Labels[rN + 1][cN].Content;
                    Labels[rN + 1][cN].Content = tmp;
                    StepsTextBox.Text = $"Шагов сделано: {++StepsCount}";
             }

                if (cN - 1 >= 0 && (cN - 1) < field.ColumnDefinitions.Count && Labels[rN][cN - 1].Content == null)
                {
                    object tmp = Labels[rN][cN].Content;
                    Labels[rN][cN].Content = Labels[rN][cN - 1].Content;
                    Labels[rN][cN - 1].Content = tmp;
                    StepsTextBox.Text = $"Шагов сделано: {++StepsCount}";
                }

                if (cN + 1 >= 0 && (cN + 1) < field.ColumnDefinitions.Count && Labels[rN][cN + 1].Content == null)
                {
                    object tmp = Labels[rN][cN].Content;
                    Labels[rN][cN].Content = Labels[rN][cN + 1].Content;
                    Labels[rN][cN + 1].Content = tmp;
                    StepsTextBox.Text = $"Шагов сделано: {++StepsCount}";
                }
                if (rN - 1 >= 0 && (rN - 1) < field.ColumnDefinitions.Count && Labels[rN - 1][cN].Content == null)
                {
                    object tmp = Labels[rN][cN].Content;
                    Labels[rN][cN].Content = Labels[rN - 1][cN].Content;
                    Labels[rN - 1][cN].Content = tmp;
                    StepsTextBox.Text = $"Шагов сделано: {++StepsCount}";
                }

            int k = 1;
            bool flag = true;

            for (int i = 0; i < Labels.Length; i++)
            {
                for(int j = 0; j < Labels[i].Length; j++, k++)
                {
                    if (Labels[j][i].Content?.ToString() != k.ToString() && Labels[j][i].Content != null)
                    {
                        flag = false;
                        break;
                    }
                }
            }

            if (flag == true)
            {
                MessageBox.Show("Успех!");
                Timer.Stop();
                StreamWriter writer = new StreamWriter("backup.txt", true, Encoding.Default);
                writer.WriteLine($"{StepsCount} {Seconds}");
                writer.Close();

                writer.Close();
                return;
            }

        }

        private void RatesButton_Click(object sender, RoutedEventArgs e)
        {
            Records records = new Records();
            records.ShowDialog();
        }
    }
}
