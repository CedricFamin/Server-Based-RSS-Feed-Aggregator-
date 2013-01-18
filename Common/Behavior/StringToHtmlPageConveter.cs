using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Common.Converter
{
    public class StringToHtmlPageConverter : IValueConverter
    {

        private string header = "<! doctype html><html><head><meta charset='UTF-8'/></head><body><div>";
        private string footer = "</div></body></html>";

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
