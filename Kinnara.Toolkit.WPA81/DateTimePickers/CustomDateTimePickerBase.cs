using System;
using System.Diagnostics.CodeAnalysis;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Represents a base class for controls that allow a user to pick a date/time value.
    /// </summary>
    [TemplatePart(Name = ElementHeaderContentPresenterName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementFlyoutButtonName, Type = typeof(ButtonBase))]
    [TemplateVisualState(GroupName = VisualStates.GroupCommon, Name = VisualStates.StateNormal)]
    [TemplateVisualState(GroupName = VisualStates.GroupCommon, Name = VisualStates.StateDisabled)]
    public abstract class CustomDateTimePickerBase : Control
    {
        private const string ElementHeaderContentPresenterName = "HeaderContentPresenter";
        private const string ElementFlyoutButtonName = "FlyoutButton";

        private DateTimeFormatter _formatter;
        private PickerFlyoutBase _flyout;

        /// <summary>
        /// Provides base class initialization behavior for CustomDateTimePickerBase-derived classes.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected CustomDateTimePickerBase()
        {
            if (DefaultFlyoutTitle != null)
            {
                FlyoutTitle = DefaultFlyoutTitle;
            }

            PlaceholderText = FlyoutTitle.ToLower();

            IsEnabledChanged += OnIsEnabledChanged;
        }

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
            typeof(CustomDateTimePickerBase),
            new PropertyMetadata(null, (d, e) => ((CustomDateTimePickerBase)d).OnHeaderChanged(e)));

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
            typeof(CustomDateTimePickerBase),
            new PropertyMetadata(null, (d, e) => ((CustomDateTimePickerBase)d).OnHeaderTemplateChanged(e)));

        private void OnHeaderTemplateChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateHeaderContentPresenterVisibility();
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
            typeof(CustomDateTimePickerBase),
            new PropertyMetadata(string.Empty, (d, e) => ((CustomDateTimePickerBase)d).OnPlaceholderTextChanged(e)));

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
            typeof(CustomDateTimePickerBase),
            new PropertyMetadata(string.Empty, (d, e) => ((CustomDateTimePickerBase)d).OnFlyoutTitleChanged(e)));

        private void OnFlyoutTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_flyout != null)
            {
                PickerFlyoutBase.SetTitle(_flyout, FlyoutTitle);
            }
        }

        #endregion

        /// <summary>
        /// Gets the picker flyout that allows a user to pick a date/time value.
        /// </summary>
        protected PickerFlyoutBase Flyout
        {
            get { return _flyout; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the date and time to be formatted.
        /// </summary>
        protected abstract DateTimeOffset? Value { get; }

        /// <summary>
        /// When overridden in a derived class, gets the display format for the date/time.
        /// </summary>
        protected abstract string Format { get; }

        /// <summary>
        /// Gets the default flyout title.
        /// </summary>
        protected virtual string DefaultFlyoutTitle
        {
            get { return string.Empty; }
        }

        private FrameworkElement ElementHeaderContentPresenter { get; set; }

        private ButtonBase ElementFlyoutButton { get; set; }

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

        /// <summary>
        /// When overridden in a derived class, initializes a picker flyout that allows a user to pick a date/time value.
        /// </summary>
        /// 
        /// <returns>
        /// A picker flyout that allows a user to pick a date/time value.
        /// </returns>
        protected abstract PickerFlyoutBase CreateFlyout();

        /// <summary>
        /// Invalidates the date/time value.
        /// </summary>
        protected void InvalidateValue()
        {
            UpdateFlyoutButtonContent();
        }

        /// <summary>
        /// Invalidates the display format for the date/time.
        /// </summary>
        protected void InvalidateFormat()
        {
            _formatter = null;
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

        private void EnsureFormatter()
        {
            if (_formatter == null)
            {
                _formatter = new DateTimeFormatter(Format);
            }
        }

        private void EnsureFlyout()
        {
            if (_flyout == null)
            {
                _flyout = CreateFlyout();
                PickerFlyoutBase.SetTitle(_flyout, FlyoutTitle);
            }
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
                var value = Value;
                if (value.HasValue)
                {
                    EnsureFormatter();
                    ElementFlyoutButton.Content = _formatter.Format(value.Value);
                }
                else
                {
                    ElementFlyoutButton.Content = PlaceholderText;
                }
            }
        }

        private void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsEnabled ? VisualStates.StateNormal : VisualStates.StateDisabled, useTransitions);
        }
    }
}
