using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Spots
{
    /// <summary>
    /// Логика взаимодействия для Records.xaml
    /// </summary>
    public partial class Records : Window
    {

        public struct PareValues : IComparable
        {
            public int Time;
            public int Steps;
            
            public PareValues(int time, int steps)
            {
                Time = time;
                Steps = steps;
            }

            int IComparable.CompareTo(object obj) => (Steps < ((PareValues)obj).Steps) ? 0 : 1;
        }
        public Records()
        {
            InitializeComponent();
            string[] records = File.ReadAllLines("backup.txt");
            List<PareValues> lst = new List<PareValues>();

            for (int i = 0; i < records.Length; i++)
            {
                string[] record = records[i].Split(' ');
                lst.Add(new PareValues(Int32.Parse(record[1]), Int32.Parse(record[0])));
            }

            lst.Sort();

            foreach (PareValues item in lst)
            {
                Label label = new Label();
                label.FontFamily = new FontFamily("Bold");
                label.FontSize = 20;
                label.Content = $"Игра пройдена за: {item.Time} секунд, шагов сделано {item.Steps}";

                TableRecords.Children.Add(label);
            }
        }
    }
}
