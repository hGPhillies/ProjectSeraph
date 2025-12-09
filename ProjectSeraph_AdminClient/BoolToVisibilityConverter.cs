using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ProjectSeraph_AdminClient
{
    /// <summary>
    /// Converts a boolean value to a Visibility value.
    /// True becomes Visible, and false becomes Collapsed.
    /// This is useful when controlling UI element visibility through ViewModel boolean properties.
    /// </summary>

    //Converts a boolean to a Visibility value. True -> Visibility.Visible, False -> Visibility.Collapsed
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
            => (value is bool b && b) 
            ? Visibility.Visible 
            : Visibility.Collapsed;

        //Converts a Visibility value back into a boolean. Visible -> true, Collapsed/Hidden -> false
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is Visibility v && v == Visibility.Visible;        
    }
}
