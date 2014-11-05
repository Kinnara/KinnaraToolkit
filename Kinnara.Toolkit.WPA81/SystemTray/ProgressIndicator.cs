using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Provides methods and properties for interacting with the progress indicator on the system tray on a page.
    /// </summary>
    public class ProgressIndicator : DependencyObject
    {
        #region public bool IsVisible

        /// <summary>
        /// Gets or sets the visibility of the progress indicator on the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// true if the progress indicator is visible; otherwise, false.
        /// </returns>
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="P:Microsoft.Phone.Shell.ProgressIndicator.IsVisible"/>.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
            "IsVisible",
            typeof(bool),
            typeof(ProgressIndicator),
            new PropertyMetadata(false, (d, e) => ((ProgressIndicator)d).OnIsVisibleChanged(e)));

        private void OnIsVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (object.ReferenceEquals(this, GetActiveProgressIndicator()))
            {
                SystemTray.UpdateStatusBarProgressIndicator();
            }
        }

        #endregion

        #region public bool IsIndeterminate

        /// <summary>
        /// Gets or sets a value that indicates whether the progress indicator on the system tray on the current page is determinate or indeterminate.
        /// </summary>
        /// 
        /// <returns>
        /// true if the progress indicator is indeterminate; false if the progress bar is determinate.
        /// </returns>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="P:Microsoft.Phone.Shell.ProgressIndicator.IsIndeterminate"/>.
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty = DependencyProperty.Register(
            "IsIndeterminate",
            typeof(bool),
            typeof(ProgressIndicator),
            new PropertyMetadata(false, (d, e) => ((ProgressIndicator)d).OnIsIndeterminateChanged(e)));

        private void OnIsIndeterminateChanged(DependencyPropertyChangedEventArgs e)
        {
            if (object.ReferenceEquals(this, GetActiveProgressIndicator()))
            {
                SystemTray.UpdateStatusBarProgressIndicator();
            }
        }

        #endregion

        #region public string Text

        /// <summary>
        /// Gets or sets the text of the progress indicator on the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The text of the progress indicator on the system tray.
        /// </returns>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="P:Microsoft.Phone.Shell.ProgressIndicator.Text"/>.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(ProgressIndicator),
            new PropertyMetadata(string.Empty, OnPropertyChanged));

        #endregion

        #region public double Value

        /// <summary>
        /// Gets or sets the value of the progress indicator on the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The value of the progress indicator on the system tray.
        /// </returns>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="P:Microsoft.Phone.Shell.ProgressIndicator.Value"/>.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(ProgressIndicator),
            new PropertyMetadata(0.0, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var value = (double)e.NewValue;
            if (value < 0 || value > 1)
            {
                ((ProgressIndicator)d).Value = ClampedProgressValue(value);
            }
            else
            {
                OnPropertyChanged(d, e);
            }
        }

        #endregion

        internal static ProgressIndicator GetActiveProgressIndicator()
        {
            Page activePage = SystemTray.TryGetActivePage();
            if (activePage != null)
            {
                return SystemTray.GetProgressIndicator(activePage);
            }
            return null;
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

            if (object.ReferenceEquals(d, GetActiveProgressIndicator()))
            {
                SystemTray.UpdateStatusBarProgressIndicator();
            }
        }

        private static double ClampedProgressValue(double value)
        {
            return Math.Min(Math.Max(value, 0.0), 1.0);
        }
    }
}
