using System;
using System.Diagnostics.CodeAnalysis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Represents a control that allows a user to pick a time value.
    /// </summary>
    public class CustomTimePicker : CustomDateTimePickerBase
    {
        private static TimePicker ReferenceTimePicker = new TimePicker();

        /// <summary>
        /// Initializes a new instance of the CustomTimePicker class.
        /// </summary>
        public CustomTimePicker()
        {
            DefaultStyleKey = typeof(CustomTimePicker);

            Time = GetCurrentTime();
        }

        #region ClockIdentifier

        /// <summary>
        /// Gets or sets the clock system to use.
        /// </summary>
        /// 
        /// <returns>
        /// The name of the clock system to use.
        /// </returns>
        public string ClockIdentifier
        {
            get { return (string)GetValue(ClockIdentifierProperty); }
            set { SetValue(ClockIdentifierProperty, value); }
        }

        /// <summary>
        /// Identifies the ClockIdentifier dependency property.
        /// </summary>
        public static readonly DependencyProperty ClockIdentifierProperty = DependencyProperty.Register(
            "ClockIdentifier",
            typeof(string),
            typeof(CustomTimePicker),
            new PropertyMetadata(ReferenceTimePicker.ClockIdentifier, (d, e) => ((CustomTimePicker)d).OnClockIdentifierChanged(e)));

        private void OnClockIdentifierChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.ClockIdentifier = ClockIdentifier;
            }
        }

        #endregion

        #region Time

        /// <summary>
        /// Gets or sets the time currently set in the time picker.
        /// </summary>
        /// 
        /// <returns>
        /// The time currently set in the time picker.
        /// </returns>
        public TimeSpan? Time
        {
            get { return (TimeSpan?)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        /// <summary>
        /// Gets the identifier for the Time dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(
            "Time",
            typeof(object),
            typeof(CustomTimePicker),
            new PropertyMetadata(null, (d, e) => ((CustomTimePicker)d).OnTimeChanged(e)));

        private void OnTimeChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldTime = (TimeSpan?)e.OldValue;
            var newTime = (TimeSpan?)e.NewValue;

            if (PickerFlyout != null)
            {
                PickerFlyout.Time = Time.GetValueOrDefault(GetCurrentTime());
            }

            InvalidateValue();

            var handler = TimeChanged;
            if (handler != null)
            {
                handler(this, new CustomTimePickerValueChangedEventArgs(oldTime, newTime));
            }
        }

        #endregion

        #region MinuteIncrement

        /// <summary>
        /// Gets or sets a value that indicates the time increments shown in the minute picker.
        /// For example, 15 specifies that the TimePicker minute control displays only the choices 00, 15, 30, 45.
        /// </summary>
        /// 
        /// <returns>
        /// An integer from 0-59 that indicates the increments shown in the minute picker. The default is 1.
        /// </returns>
        public int MinuteIncrement
        {
            get { return (int)GetValue(MinuteIncrementProperty); }
            set { SetValue(MinuteIncrementProperty, value); }
        }

        /// <summary>
        /// Identifies the MinuteIncrement dependency property.
        /// </summary>
        public static readonly DependencyProperty MinuteIncrementProperty = DependencyProperty.Register(
            "MinuteIncrement",
            typeof(int),
            typeof(CustomTimePicker),
            new PropertyMetadata(ReferenceTimePicker.MinuteIncrement, (d, e) => ((CustomTimePicker)d).OnMinuteIncrementChanged(e)));

        private void OnMinuteIncrementChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.MinuteIncrement = MinuteIncrement;
            }
        }

        #endregion

        #region TimeFormat

        /// <summary>
        /// Gets or sets the display format for the time.
        /// </summary>
        /// 
        /// <returns>
        /// The display format for the time.
        /// </returns>
        public string TimeFormat
        {
            get { return (string)GetValue(TimeFormatProperty); }
            set { SetValue(TimeFormatProperty, value); }
        }

        /// <summary>
        /// Identifies the TimeFormat dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeFormatProperty = DependencyProperty.Register(
            "TimeFormat",
            typeof(string),
            typeof(CustomTimePicker),
            new PropertyMetadata("shorttime", (d, e) => ((CustomTimePicker)d).OnTimeFormatChanged(e)));

        private void OnTimeFormatChanged(DependencyPropertyChangedEventArgs e)
        {
            InvalidateFormat();
        }

        #endregion

        /// <summary>
        /// Gets the date and time to be formatted.
        /// </summary>
        protected override DateTimeOffset? Value
        {
            get
            {
                if (Time.HasValue)
                {
                    return new DateTimeOffset(DateTime.Today.Add(Time.Value));
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the display format for the time.
        /// </summary>
        protected override string Format
        {
            get { return TimeFormat; }
        }

        /// <summary>
        /// Gets the default flyout title.
        /// </summary>
        protected override string DefaultFlyoutTitle
        {
            get { return ControlResources.TimePickerTitleText; }
        }

        private TimePickerFlyout PickerFlyout
        {
            get { return Flyout as TimePickerFlyout; }
        }

        /// <summary>
        /// Occurs when the time value is changed.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event EventHandler<CustomTimePickerValueChangedEventArgs> TimeChanged;

        /// <summary>
        /// Initializes a picker flyout that allows a user to pick a time value.
        /// </summary>
        /// 
        /// <returns>
        /// A picker flyout that allows a user to pick a time value.
        /// </returns>
        protected override PickerFlyoutBase CreateFlyout()
        {
            var flyout = new TimePickerFlyout
            {
                ClockIdentifier = ClockIdentifier,
                Time = Time.GetValueOrDefault(GetCurrentTime()),
                MinuteIncrement = MinuteIncrement
            };
            flyout.TimePicked += OnFlyoutTimePicked;
            return flyout;
        }

        private void OnFlyoutTimePicked(TimePickerFlyout sender, TimePickedEventArgs args)
        {
            Time = args.NewTime;
        }

        private static TimeSpan GetCurrentTime()
        {
            return DateTimeOffset.Now.TimeOfDay;
        }
    }
}
