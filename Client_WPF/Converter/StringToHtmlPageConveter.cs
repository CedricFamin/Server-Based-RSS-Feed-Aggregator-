using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Client_WPF.Converter
{
    public class StringToHtmlPageConverter : IValueConverter
    {

        private string header = "<html><head></head><body>";
        private string footer = "</body></html>";

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = value as string;
            strValue = header + strValue + footer;
            return strValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
