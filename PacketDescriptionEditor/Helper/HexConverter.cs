using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PacketDescriptionEditor.Helper
{
    public class HexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            byte b = (byte)value;
            return b.ToString("X");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return System.Convert.ToByte((string)value, 16);
            }
            catch (FormatException)
            {
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
