using System;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Provides event data for the CustomDatePicker.DateChanged event.
    /// </summary>
    public sealed class CustomDatePickerValueChangedEventArgs
    {
        internal CustomDatePickerValueChangedEventArgs(DateTimeOffset? oldDate, DateTimeOffset? newDate)
        {
            OldDate = oldDate;
            NewDate = newDate;
        }

        /// <summary>
        /// Gets the new date selected in the picker.
        /// </summary>
        /// 
        /// <returns>
        /// The new date selected in the picker.
        /// </returns>
        public DateTimeOffset? NewDate { get; private set; }

        /// <summary>
        /// Gets the date previously selected in the picker.
        /// </summary>
        /// 
        /// <returns>
        /// The date previously selected in the picker.
        /// </returns>
        public DateTimeOffset? OldDate { get; private set; }
    }
}
