using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPF_MovingObjects
{
    [Serializable]
    public class FigureContainer
    {
        public MainWindow.Figure Type;
        public double Width, Height;
        public double? Angle;
        public double? Scale;
        public double X, Y;
        public byte R, G, B;
        public FigureContainer() { }
    }
}
