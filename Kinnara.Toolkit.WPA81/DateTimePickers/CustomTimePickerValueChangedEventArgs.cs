using System;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Provides event data for the CustomTimePicker.TimeChanged event.
    /// </summary>
    public sealed class CustomTimePickerValueChangedEventArgs
    {
        internal CustomTimePickerValueChangedEventArgs(TimeSpan? oldTime, TimeSpan? newTime)
        {
            OldTime = oldTime;
            NewTime = newTime;
        }

        /// <summary>
        /// Gets the new time selected in the picker.
        /// </summary>
        /// 
        /// <returns>
        /// The new time selected in the picker.
        /// </returns>
        public TimeSpan? NewTime { get; private set; }

        /// <summary>
        /// Gets the time previously selected in the picker.
        /// </summary>
        /// 
        /// <returns>
        /// The time previously selected in the picker.
        /// </returns>
        public TimeSpan? OldTime { get; private set; }
    }
}
