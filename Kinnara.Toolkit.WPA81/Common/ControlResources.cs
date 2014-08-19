using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace Kinnara.Xaml.Controls
{
    internal class ControlResources
    {
        private static ResourceLoader _resourceLoader = ResourceLoader.GetForViewIndependentUse("Kinnara.Toolkit/Resources");

        public static string DatePickerTitleText
        {
            get { return GetString(); }
        }

        public static string TimePickerTitleText
        {
            get { return GetString(); }
        }

        private static string GetString([CallerMemberName]string resource = null)
        {
            return _resourceLoader.GetString(resource);
        }
    }
}
