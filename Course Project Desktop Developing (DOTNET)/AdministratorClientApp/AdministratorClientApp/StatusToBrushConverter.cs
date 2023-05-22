using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using AdministratorClientApp.ServiceReference;

namespace AdministratorClientApp
{
    public class StatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is ResourcesNetworkStatus))
            {
                return null;
            }

            switch ((ResourcesNetworkStatus)value)
            {
                case ResourcesNetworkStatus.Online:
                    return new SolidColorBrush(Colors.Green);
                case ResourcesNetworkStatus.Offline:
                    return new SolidColorBrush(Colors.Gray);
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
