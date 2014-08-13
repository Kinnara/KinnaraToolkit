using System;
using Windows.UI.Xaml.Data;

namespace Kinnara.Xaml.Controls
{
    public sealed class StringToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string s = value as string;
            if (s != null)
            {
                return s.ToUpper();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
