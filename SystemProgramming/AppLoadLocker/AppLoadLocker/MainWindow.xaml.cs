using System;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace AppLoadLocker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> BlackList;
        private DispatcherTimer Timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += TimerOnTick;
            Timer.Start();
            this.Hide();

            BlackList = File.ReadLines($"{Environment.CurrentDirectory}/BlackList.txt").ToList();

        }

        private void TimerOnTick(object? sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            string[] processesName = (from process in processes select process.ProcessName).ToArray();

            foreach (var process in processesName.Intersect(BlackList))
               Process.GetProcessesByName(process).First().Kill(); 
            
            
        }
    }
}