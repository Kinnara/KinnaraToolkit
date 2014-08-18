using System;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Represents a control that allows a user to pick a date value.
    /// </summary>
    [TemplatePart(Name = ElementHeaderContentPresenterName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementFlyoutButtonName, Type = typeof(ButtonBase))]
    [TemplateVisualState(GroupName = VisualStates.GroupCommon, Name = VisualStates.StateNormal)]
    [TemplateVisualState(GroupName = VisualStates.GroupCommon, Name = VisualStates.StateDisabled)]
    public class CustomDatePicker : Control
    {
        private const string ElementHeaderContentPresenterName = "HeaderContentPresenter";
        private const string ElementFlyoutButtonName = "FlyoutButton";

        private static DatePicker ReferenceDatePicker = new DatePicker();

        private DateTimeFormatter _dateFormatter;
        private DatePickerFlyout _flyout;

        /// <summary>
        /// Initializes a new instance of the DatePicker class.
        /// </summary>
        public CustomDatePicker()
        {
            DefaultStyleKey = typeof(CustomDatePicker);

            ResetDateFormatter();
            PlaceholderText = ControlResources.DatePickerTitleText.ToLower();

            Date = DateTimeOffset.Now;

            IsEnabledChanged += OnIsEnabledChanged;
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
        /// Gets the identifier for the CalendarIdentifier dependency property.
        /// </summary>
        public static readonly DependencyProperty CalendarIdentifierProperty = DependencyProperty.Register(
            "CalendarIdentifier",
            typeof(string),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.CalendarIdentifier, (d, e) => ((CustomDatePicker)d).OnCalendarIdentifierChanged(e)));

        private void OnCalendarIdentifierChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.CalendarIdentifier = CalendarIdentifier;
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
        /// Gets the identifier for the Date dependency property.
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

            if (_flyout != null)
            {
                _flyout.Date = Date.GetValueOrDefault(DateTimeOffset.Now);
            }

            UpdateFlyoutButtonContent();

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
        /// Gets the identifier for the DayVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty DayVisibleProperty = DependencyProperty.Register(
            "DayVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.DayVisible, (d, e) => ((CustomDatePicker)d).OnDayVisibleChanged(e)));

        private void OnDayVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.DayVisible = DayVisible;
            }
        }

        #endregion

        #region Header

        /// <summary>
        /// Gets or sets the content for the control's header.
        /// </summary>
        /// 
        /// <returns>
        /// The content of the control's header. The default is null.
        /// </returns>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Identifies the Header dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(object),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.Header, (d, e) => ((CustomDatePicker)d).OnHeaderChanged(e)));

        private void OnHeaderChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateHeaderContentPresenterVisibility();
        }

        #endregion

        #region HeaderTemplate

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the control's header.
        /// </summary>
        /// 
        /// <returns>
        /// The template that specifies the visualization of the header object. The default is null.
        /// </returns>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the HeaderTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = DependencyProperty.Register(
            "HeaderTemplate",
            typeof(DataTemplate),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.HeaderTemplate, (d, e) => ((CustomDatePicker)d).OnHeaderTemplateChanged(e)));

        private void OnHeaderTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateHeaderContentPresenterVisibility();
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
        /// Gets the identifier for the MaxYear dependency property.
        /// </summary>
        public static readonly DependencyProperty MaxYearProperty = DependencyProperty.Register(
            "MaxYear",
            typeof(DateTimeOffset),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MaxYear, (d, e) => ((CustomDatePicker)d).OnMaxYearChanged(e)));

        private void OnMaxYearChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.MaxYear = MaxYear;
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
        /// Gets the identifier for the MinYear dependency property.
        /// </summary>
        public static readonly DependencyProperty MinYearProperty = DependencyProperty.Register(
            "MinYear",
            typeof(DateTimeOffset),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MinYear, (d, e) => ((CustomDatePicker)d).OnMinYearChanged(e)));

        private void OnMinYearChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.MinYear = MinYear;
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
        /// Gets the identifier for the MonthVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty MonthVisibleProperty = DependencyProperty.Register(
            "MonthVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.MonthVisible, (d, e) => ((CustomDatePicker)d).OnMonthVisibleChanged(e)));

        private void OnMonthVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.MonthVisible = MonthVisible;
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
        /// Gets the identifier for the YearVisible dependency property.
        /// </summary>
        public static readonly DependencyProperty YearVisibleProperty = DependencyProperty.Register(
            "YearVisible",
            typeof(bool),
            typeof(CustomDatePicker),
            new PropertyMetadata(ReferenceDatePicker.YearVisible, (d, e) => ((CustomDatePicker)d).OnYearVisibleChanged(e)));

        private void OnYearVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                _flyout.YearVisible = YearVisible;
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
            ResetDateFormatter();
            UpdateFlyoutButtonContent();
        }

        #endregion

        #region PlaceholderText

        /// <summary>
        /// Gets or sets the text that is displayed in the control until the value is changed by a user action or some other operation.
        /// </summary>
        /// 
        /// <returns>
        /// The text that is displayed in the control when no value is selected. The default is an empty string ("").
        /// </returns>
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        /// <summary>
        /// Identifies the PlaceholderText dependency property.
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
            "PlaceholderText",
            typeof(string),
            typeof(CustomDatePicker),
            new PropertyMetadata(string.Empty, (d, e) => ((CustomDatePicker)d).OnPlaceholderTextChanged(e)));

        private void OnPlaceholderTextChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateFlyoutButtonContent();
        }

        #endregion

        #region FlyoutTitle

        /// <summary>
        /// Gets or sets the title for a picker flyout when it appears.
        /// </summary>
        /// 
        /// <returns>
        /// The title for a picker flyout when it appears.
        /// </returns>
        public string FlyoutTitle
        {
            get { return (string)GetValue(FlyoutTitleProperty); }
            set { SetValue(FlyoutTitleProperty, value); }
        }

        /// <summary>
        /// Identifies the FlyoutTitle dependency property.
        /// </summary>
        public static readonly DependencyProperty FlyoutTitleProperty = DependencyProperty.Register(
            "FlyoutTitle",
            typeof(string),
            typeof(CustomDatePicker),
            new PropertyMetadata(string.Empty, (d, e) => ((CustomDatePicker)d).OnFlyoutTitleChanged(e)));

        private void OnFlyoutTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                PickerFlyoutBase.SetTitle(_flyout, FlyoutTitle);
            }
        }

        #endregion

        private FrameworkElement ElementHeaderContentPresenter { get; set; }

        private ButtonBase ElementFlyoutButton { get; set; }

        /// <summary>
        /// Occurs when the date value is changed.
        /// </summary>
        public event EventHandler<CustomDatePickerValueChangedEventArgs> DateChanged;

        /// <summary>
        /// Builds the visual tree for the control when a new template is applied.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ElementFlyoutButton != null)
            {
                ElementFlyoutButton.Click -= OnFlyoutButtonClick;
            }

            ElementHeaderContentPresenter = GetTemplateChild(ElementHeaderContentPresenterName) as FrameworkElement;
            ElementFlyoutButton = GetTemplateChild(ElementFlyoutButtonName) as ButtonBase;

            if (ElementFlyoutButton != null)
            {
                ElementFlyoutButton.Click += OnFlyoutButtonClick;
            }

            UpdateVisualState(false);
            UpdateHeaderContentPresenterVisibility();
            UpdateFlyoutButtonContent();
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateVisualState(true);
        }

        private void OnFlyoutButtonClick(object sender, RoutedEventArgs e)
        {
            EnsureFlyout();
            _flyout.ShowAt(this);
        }

        private void OnFlyoutDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            Date = args.NewDate;
        }

        private void ResetDateFormatter()
        {
            _dateFormatter = new DateTimeFormatter(DateFormat);
        }

        private void EnsureFlyout()
        {
            if (_flyout == null)
            {
                _flyout = new DatePickerFlyout
                {
                    CalendarIdentifier = CalendarIdentifier,
                    Date = Date.GetValueOrDefault(DateTimeOffset.Now),
                    DayVisible = DayVisible,
                    MaxYear = MaxYear,
                    MinYear = MinYear,
                    MonthVisible = MonthVisible,
                    YearVisible = YearVisible
                };
                PickerFlyoutBase.SetTitle(_flyout, !string.IsNullOrEmpty(FlyoutTitle) ? FlyoutTitle : ControlResources.DatePickerTitleText);
                _flyout.DatePicked += OnFlyoutDatePicked;
            }
        }

        private void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsEnabled ? VisualStates.StateNormal : VisualStates.StateDisabled, useTransitions);
        }

        private void UpdateHeaderContentPresenterVisibility()
        {
            if (ElementHeaderContentPresenter != null)
            {
                ElementHeaderContentPresenter.Visibility = Header == null && HeaderTemplate == null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private void UpdateFlyoutButtonContent()
        {
            if (ElementFlyoutButton != null)
            {
                if (Date.HasValue)
                {
                    ElementFlyoutButton.Content = _dateFormatter.Format(Date.Value);
                }
                else
                {
                    ElementFlyoutButton.Content = PlaceholderText;
                }
            }
        }
    }
}
