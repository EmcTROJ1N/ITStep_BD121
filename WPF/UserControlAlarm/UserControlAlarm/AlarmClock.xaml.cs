using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace UserControlAlarm
{
    /// <summary>
    /// Логика взаимодействия для AlarmClock.xaml
    /// </summary>
    public partial class AlarmClock : UserControl, INotifyPropertyChanged
    {
        public delegate void AlarmHandler(object sender, EventArgs e);
        public double Bord { get => Diameter / 20; }
        public double ArrowThickness { get => Diameter / 20; }
        public double Diameter
        {
            get => Width;
            set
            {
                Width = value;
                Height = value; 
            }
        }

        double _HourArrayAngle;
        public double HourArrowAngle
        {
            get => _HourArrayAngle;
            set
            {
                _HourArrayAngle = value;
                OnPropertyChanged("HourArrowAngle");
            }
        }

        double _MinuteArrayAngle;
        public double MinuteArrowAngle
        {
            get => _MinuteArrayAngle;
            set
            {
                _MinuteArrayAngle = value;
                OnPropertyChanged("MinuteArrayAngle");
            }
        }

        double _SecondsArrowAngle;
        public double SecondsArrowAngle
        {
            get => _SecondsArrowAngle;
            set
            {
                _SecondsArrowAngle = value;
                OnPropertyChanged("SecondsArrowAngle");
            }
        }
        public Thickness HourArrowThickness { get => new Thickness(0, Diameter / 5, 0, Diameter / 2.5); }
        DispatcherTimer TickTimer = new DispatcherTimer();
        public List<DateTime> Alarms = new List<DateTime>();
        public AlarmHandler Handler;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public AlarmClock()
        {
            InitializeComponent();
            Forma.DataContext = this;
            HourArrow.DataContext = this;
            MinuteArrow.DataContext = this;
            SecondsArrow.DataContext = this;
            TickTimer.Interval = TimeSpan.FromSeconds(1);
            TickTimer.Tick += TickTimer_Tick;
            TickTimer.Start();

            DateTime currentDate = DateTime.Now;
            
            HourArrowAngle = (360 / 12) * (currentDate.Hour > 12 ? 12 - currentDate.Hour : currentDate.Hour);
            MinuteArrowAngle = (360 / 60) * currentDate.Minute;
            SecondsArrowAngle = (360 / 60) * currentDate.Second;
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            
            foreach (var alarm in Alarms)
            {
                if (alarm.ToLongTimeString() == currentDate.ToLongTimeString())
                    Handler?.Invoke(this, null);
            }

            HourArrowAngle = (360 / 12) * (currentDate.Hour > 12 ? 12 - currentDate.Hour : currentDate.Hour);
            MinuteArrowAngle = (360 / 60) * currentDate.Minute;
            SecondsArrowAngle = (360 / 60) * currentDate.Second;
        }
    }
}
