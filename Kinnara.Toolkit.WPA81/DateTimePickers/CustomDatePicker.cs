using System;
using System.Diagnostics.CodeAnalysis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Represents a control that allows a user to pick a date value.
    /// </summary>
    public class CustomDatePicker : CustomDateTimePickerBase
    {
        private static DatePicker ReferenceDatePicker = new DatePicker();

        /// <summary>
        /// Initializes a new instance of the CustomDatePicker class.
        /// </summary>
        public CustomDatePicker()
        {
            DefaultStyleKey = typeof(CustomDatePicker);

            Date = GetCurrentDate();
        }

        #region CalendarIdentifier

        /// <summary>
        /// Gets or sets the calendar system to use.
        /// </summary>
        /// 
        /// <returns>
        /// The calendar system to use.
        /// </returns>
        public string CalendarIdentifier
        {
            get { return (string)GetValue(CalendarIdentifierProperty); }
            set { SetValue(CalendarIdentifierProperty, value); }
        }

        /// <summary>
        /// Identifies the CalendarIdentifier dependency property.
        /// </summary>
        public static readonly DependencyProperty CalendarIdentifierProperty = DependencyProperty.Register(
            "CalendarIdentifier",
            typeof(string),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.CalendarIdentifier, (d, e) => ((CustomDatePicker)d).OnCalendarIdentifierChanged(e)));

        private void OnCalendarIdentifierChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.CalendarIdentifier = CalendarIdentifier;
            }
        }

        #endregion

        #region Date

        /// <summary>
        /// Gets or sets the date currently set in the date picker.
        /// </summary>
        /// 
        /// <returns>
        /// The date currently set in the picker.
        /// </returns>
        public DateTimeOffset? Date
        {
            get { return (DateTimeOffset?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        /// <summary>
        /// Identifies the Date dependency property.
        /// </summary>
        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            "Date",
            typeof(object),
            typeof(CustomDatePicker),
            new PropertyMetadata(null, (d, e) => ((CustomDatePicker)d).OnDateChanged(e)));

        private void OnDateChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldDate = (DateTimeOffset?)e.OldValue;
            var newDate = (DateTimeOffset?)e.NewValue;

            if (PickerFlyout != null)
            {
                PickerFlyout.Date = Date.GetValueOrDefault(GetCurrentDate());
            }

            InvalidateValue();

            var handler = DateChanged;
            if (handler != null)
            {
                handler(this, new CustomDatePickerValueChangedEventArgs(oldDate, newDate));
            }
        }

        #endregion

        #region DayVisible

        /// <summary>
        /// Gets or sets a value that indicates whether the day selector is shown.
        /// </summary>
        /// 
        /// <returns>
        /// True if the day selector is shown; otherwise, false. The default is true.
        /// </returns>
        public bool DayVisible
        {
            get { return (bool)GetValue(DayVisibleProperty); }
            set { SetValue(DayVisibleProperty, value); }
        }

        /// <summary>
        /// Identifies the DayVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty DayVisibleProperty = DependencyProperty.Register(
            "DayVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.DayVisible, (d, e) => ((CustomDatePicker)d).OnDayVisibleChanged(e)));

        private void OnDayVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.DayVisible = DayVisible;
            }
        }

        #endregion

        #region MaxYear

        /// <summary>
        /// Gets or sets the maximum Gregorian year available for picking.
        /// </summary>
        /// 
        /// <returns>
        /// The maximum Gregorian year available for picking.
        /// </returns>
        public DateTimeOffset MaxYear
        {
            get { return (DateTimeOffset)GetValue(MaxYearProperty); }
            set { SetValue(MaxYearProperty, value); }
        }

        /// <summary>
        /// Identifies the MaxYear dependency property.
        /// </summary>
        public static readonly DependencyProperty MaxYearProperty = DependencyProperty.Register(
            "MaxYear",
            typeof(DateTimeOffset),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MaxYear, (d, e) => ((CustomDatePicker)d).OnMaxYearChanged(e)));

        private void OnMaxYearChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.MaxYear = MaxYear;
            }
        }

        #endregion

        #region MinYear

        /// <summary>
        /// Gets or sets the minimum Gregorian year available for picking.
        /// </summary>
        /// 
        /// <returns>
        /// The minimum Gregorian year available for picking.
        /// </returns>
        public DateTimeOffset MinYear
        {
            get { return (DateTimeOffset)GetValue(MinYearProperty); }
            set { SetValue(MinYearProperty, value); }
        }

        /// <summary>
        /// Identifies the MinYear dependency property.
        /// </summary>
        public static readonly DependencyProperty MinYearProperty = DependencyProperty.Register(
            "MinYear",
            typeof(DateTimeOffset),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MinYear, (d, e) => ((CustomDatePicker)d).OnMinYearChanged(e)));

        private void OnMinYearChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.MinYear = MinYear;
            }
        }

        #endregion

        #region MonthVisible

        /// <summary>
        /// Gets or sets a value that indicates whether the month selector is shown.
        /// </summary>
        /// 
        /// <returns>
        /// True if the month selector is shown; otherwise, false. The default is true.
        /// </returns>
        public bool MonthVisible
        {
            get { return (bool)GetValue(MonthVisibleProperty); }
            set { SetValue(MonthVisibleProperty, value); }
        }

        /// <summary>
        /// Identifies the MonthVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty MonthVisibleProperty = DependencyProperty.Register(
            "MonthVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MonthVisible, (d, e) => ((CustomDatePicker)d).OnMonthVisibleChanged(e)));

        private void OnMonthVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.MonthVisible = MonthVisible;
            }
        }

        #endregion

        #region YearVisible

        /// <summary>
        /// Gets or sets a value that indicates whether the year selector is shown.
        /// </summary>
        /// 
        /// <returns>
        /// True if the year selector is shown; otherwise, false. The default is true.
        /// </returns>
        public bool YearVisible
        {
            get { return (bool)GetValue(YearVisibleProperty); }
            set { SetValue(YearVisibleProperty, value); }
        }

        /// <summary>
        /// Identifies the YearVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty YearVisibleProperty = DependencyProperty.Register(
            "YearVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.YearVisible, (d, e) => ((CustomDatePicker)d).OnYearVisibleChanged(e)));

        private void OnYearVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (PickerFlyout != null)
            {
                PickerFlyout.YearVisible = YearVisible;
            }
        }

        #endregion

        #region DateFormat

        /// <summary>
        /// Gets or sets the display format for the date.
        /// </summary>
        /// 
        /// <returns>
        /// The display format for the date.
        /// </returns>
        public string DateFormat
        {
            get { return (string)GetValue(DateFormatProperty); }
            set { SetValue(DateFormatProperty, value); }
        }

        /// <summary>
        /// Identifies the DateFormat dependency property.
        /// </summary>
        public static readonly DependencyProperty DateFormatProperty = DependencyProperty.Register(
            "DateFormat",
            typeof(string),
            typeof(CustomDatePicker),
            new PropertyMetadata("shortdate", (d, e) => ((CustomDatePicker)d).OnDateFormatChanged(e)));

        private void OnDateFormatChanged(DependencyPropertyChangedEventArgs e)
        {
            InvalidateFormat();
        }

        #endregion

        /// <summary>
        /// Gets the date and time to be formatted.
        /// </summary>
        protected override DateTimeOffset? Value
        {
            get { return Date; }
        }

        /// <summary>
        /// Gets the display format for the date.
        /// </summary>
        protected override string Format
        {
            get { return DateFormat; }
        }

        /// <summary>
        /// Gets the default flyout title.
        /// </summary>
        protected override string DefaultFlyoutTitle
        {
            get { return ControlResources.DatePickerTitleText; }
        }

        private DatePickerFlyout PickerFlyout
        {
            get { return Flyout as DatePickerFlyout; }
        }

        /// <summary>
        /// Occurs when the date value is changed.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event EventHandler<CustomDatePickerValueChangedEventArgs> DateChanged;

        /// <summary>
        /// Initializes a picker flyout that allows a user to pick a date value.
        /// </summary>
        /// 
        /// <returns>
        /// A picker flyout that allows a user to pick a date value.
        /// </returns>
        protected override PickerFlyoutBase CreateFlyout()
        {
            var flyout = new DatePickerFlyout
            {
                CalendarIdentifier = CalendarIdentifier,
                Date = Date.GetValueOrDefault(GetCurrentDate()),
                DayVisible = DayVisible,
                MaxYear = MaxYear,
                MinYear = MinYear,
                MonthVisible = MonthVisible,
                YearVisible = YearVisible
            };
            flyout.DatePicked += OnFlyoutDatePicked;
            return flyout;
        }

        private void OnFlyoutDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            Date = args.NewDate;
        }

        private static DateTimeOffset GetCurrentDate()
        {
            return DateTimeOffset.Now;
        }
    }
}
