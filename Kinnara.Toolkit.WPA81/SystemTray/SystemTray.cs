using System;
using Windows.ApplicationModel;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Provides methods and properties for interacting with the system tray on a page.
    /// </summary>
    public class SystemTray : DependencyObject
    {
        private static bool _initialized;
        private static StatusBar _statusBar;
        private static Frame _frame;

        #region IsVisible

        /// <summary>
        /// Gets or sets the visibility of the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// true if the system tray is visible; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">There is no active <see cref="T:Windows.UI.Xaml.Controls.Page"/> object.</exception>
        public static bool IsVisible
        {
            get { return GetIsVisible(GetActivePage()); }
            set { SetIsVisible(GetActivePage(), value); }
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.IsVisible"/> attached property for a specified page.
        /// </summary>
        /// 
        /// <returns>
        /// true if the system tray is visible; otherwise, false.
        /// </returns>
        /// <param name="element">The page for which to get the <see cref="P:Kinnara.Xaml.Controls.SystemTray.IsVisible"/> attached property.</param>
        public static bool GetIsVisible(DependencyObject element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.IsVisible"/> attached property for a given page.
        /// </summary>
        /// <param name="element">The page for which to set the <see cref="P:Kinnara.Xaml.Controls.SystemTray.IsVisible"/> attached property.</param>
        /// <param name="isVisible">true to display the system tray; false to hide the system tray. </param>
        public static void SetIsVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="P:Kinnara.Xaml.Controls.SystemTray.IsVisible"/>.
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "IsVisible",
            typeof(bool),
            typeof(SystemTray),
            new PropertyMetadata(true, OnPropertyChanged));

        #endregion

        #region Opacity

        /// <summary>
        /// Gets or sets the opacity factor of the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The opacity factor of the system tray on the current page.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">There is no active <see cref="T:Windows.UI.Xaml.Controls.Page"/> object.</exception>
        public static double Opacity
        {
            get { return GetOpacity(GetActivePage()); }
            set { SetOpacity(GetActivePage(), value); }
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.Opacity"/> attached property for a specified page.
        /// </summary>
        /// 
        /// <returns>
        /// The opacity factor of the system tray on the current page.
        /// </returns>
        /// <param name="element">The page for which to get the <see cref="P:Kinnara.Xaml.Controls.SystemTray.Opacity"/> attached property.</param>
        public static double GetOpacity(DependencyObject element)
        {
            return (double)element.GetValue(OpacityProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.Opacity"/> attached property for a specified page.
        /// </summary>
        /// <param name="element">The page for which to set the <see cref="P:Kinnara.Xaml.Controls.SystemTray.Opacity"/> attached property.</param>
        /// <param name="opacity">The opacity factor to set for the system tray.</param>
        public static void SetOpacity(DependencyObject element, double value)
        {
            element.SetValue(OpacityProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="P:Kinnara.Xaml.Controls.SystemTray.Opacity"/>.
        /// </summary>
        public static readonly DependencyProperty OpacityProperty = DependencyProperty.RegisterAttached(
            "Opacity",
            typeof(double),
            typeof(SystemTray),
            new PropertyMetadata(1.0, OnOpacityChanged));

        private static void OnOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var opacity = (double)e.NewValue;
            if (opacity < 0 || opacity > 1)
            {
                SetOpacity(d, ClampedOpacity(opacity));
            }
            else
            {
                OnPropertyChanged(d, e);
            }
        }

        private static double ClampedOpacity(double opacity)
        {
            return Math.Min(Math.Max(opacity, 0), 1);
        }

        #endregion

        #region ForegroundColor

        /// <summary>
        /// Gets or sets the foreground color of the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The foreground color of the system tray on the current page.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">There is no active <see cref="T:Windows.UI.Xaml.Controls.Page"/> object.</exception>
        public static Color ForegroundColor
        {
            get { return GetForegroundColor(GetActivePage()); }
            set { SetForegroundColor(GetActivePage(), value); }
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ForegroundColor"/> attached property for a specified page.
        /// </summary>
        /// 
        /// <returns>
        /// The foreground color of the system tray on the current page.
        /// </returns>
        /// <param name="element">The page for which to get the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ForegroundColor"/> attached property.</param>
        public static Color GetForegroundColor(DependencyObject element)
        {
            return (Color)element.GetValue(ForegroundColorProperty);
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ForegroundColor"/> attached property for a specified page..
        /// </summary>
        /// <param name="element">The page for which to set the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ForegroundColor"/> attached property.</param>
        /// <param name="color">The foreground color to set for the system tray.</param>
        public static void SetForegroundColor(DependencyObject element, Color value)
        {
            element.SetValue(ForegroundColorProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="P:Kinnara.Xaml.Controls.SystemTray.ForegroundColor"/>.
        /// </summary>
        public static readonly DependencyProperty ForegroundColorProperty = DependencyProperty.RegisterAttached(
            "ForegroundColor",
            typeof(Color),
            typeof(SystemTray),
            new PropertyMetadata(default(Color), OnPropertyChanged));

        #endregion

        #region BackgroundColor

        /// <summary>
        /// Gets or sets the background color of the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The background color of the system tray on the current page.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">There is no active <see cref="T:Windows.UI.Xaml.Controls.Page"/> object.</exception>
        public static Color BackgroundColor
        {
            get { return GetBackgroundColor(GetActivePage()); }
            set { SetBackgroundColor(GetActivePage(), value); }
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.BackgroundColor"/> attached property for a specified page.
        /// </summary>
        /// 
        /// <returns>
        /// The background color of the system tray on the current page.
        /// </returns>
        /// <param name="element">The page for which to get the <see cref="P:Kinnara.Xaml.Controls.SystemTray.BackgroundColor"/> attached property.</param>
        public static Color GetBackgroundColor(DependencyObject element)
        {
            return (Color)element.GetValue(BackgroundColorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.BackgroundColor"/> attached property for a specified page.
        /// </summary>
        /// <param name="element">The page for which to set the <see cref="P:Kinnara.Xaml.Controls.SystemTray.BackgroundColor"/> attached property.</param>
        /// <param name="color">The background color to set for the system tray.</param>
        public static void SetBackgroundColor(DependencyObject element, Color value)
        {
            element.SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="P:Kinnara.Xaml.Controls.SystemTray.BackgroundColor"/>.
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.RegisterAttached(
            "BackgroundColor",
            typeof(Color),
            typeof(SystemTray),
            new PropertyMetadata(default(Color), OnPropertyChanged));

        #endregion

        #region ProgressIndicator

        /// <summary>
        /// Gets or sets the progress indicator on the system tray on the current page.
        /// </summary>
        /// 
        /// <returns>
        /// The progress indicator on the system tray on the current page.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">There is no active <see cref="T:Windows.UI.Xaml.Controls.Page"/> object.</exception>
        public static ProgressIndicator ProgressIndicator
        {
            get { return GetProgressIndicator(GetActivePage()); }
            set { SetProgressIndicator(GetActivePage(), value); }
        }

        /// <summary>
        /// Gets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ProgressIndicator"/> attached property for a specified page.
        /// </summary>
        /// 
        /// <returns>
        /// The progress indicator on the system tray on the current page.
        /// </returns>
        /// <param name="element">The page for which to get the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ProgressIndicator"/> attached property.</param>
        public static ProgressIndicator GetProgressIndicator(DependencyObject element)
        {
            return (ProgressIndicator)element.GetValue(ProgressIndicatorProperty);
        }

        /// <summary>
        /// Sets the value of the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ProgressIndicator"/> attached property for a specified page.
        /// </summary>
        /// <param name="element">The page for which to set the <see cref="P:Kinnara.Xaml.Controls.SystemTray.ProgressIndicator"/> attached property.</param>
        /// <param name="progressIndicator">The progress indicator to set on the system tray.</param>
        public static void SetProgressIndicator(DependencyObject element, ProgressIndicator value)
        {
            element.SetValue(ProgressIndicatorProperty, value);
        }

        /// <summary>
        /// The dependency property for <see cref="P:Kinnara.Xaml.Controls.SystemTray.ProgressIndicator"/>.
        /// </summary>
        public static readonly DependencyProperty ProgressIndicatorProperty = DependencyProperty.RegisterAttached(
            "ProgressIndicator",
            typeof(ProgressIndicator),
            typeof(SystemTray),
            new PropertyMetadata(null, OnPropertyChanged));

        #endregion

        /// <summary>
        /// Gets or sets the Frame to which this SystemTray is attached.
        /// </summary>
        public static Frame Frame
        {
            get { return _frame; }
            set
            {
                if (_frame != value)
                {
                    var oldFrame = _frame;
                    if (oldFrame != null)
                    {
                        oldFrame.Navigated -= OnFrameNavigated;
                    }

                    _frame = value;

                    if (_frame != null)
                    {
                        _frame.Navigated += OnFrameNavigated;
                        Initialize();
                    }

                    UpdateStatusBar();
                }
            }
        }

        private static void UpdateStatusBar()
        {
            Page activePage = TryGetActivePage();
            if (activePage == null)
            {
                return;
            }

            if (_statusBar == null)
            {
                _statusBar = StatusBar.GetForCurrentView();
            }

            if (GetIsVisible(activePage))
            {
                var asyncAction = _statusBar.ShowAsync();
            }
            else
            {
                var asyncAction = _statusBar.HideAsync();
            }

            _statusBar.BackgroundOpacity = GetOpacity(activePage);

            Color foregroundColor = GetForegroundColor(activePage);
            if (foregroundColor == default(Color))
            {
                _statusBar.ForegroundColor = null;
            }
            else
            {
                _statusBar.ForegroundColor = foregroundColor;
            }

            Color backgroundColor = GetBackgroundColor(activePage);
            if (backgroundColor == default(Color))
            {
                _statusBar.BackgroundColor = null;
            }
            else
            {
                _statusBar.BackgroundColor = backgroundColor;
            }

            UpdateStatusBarProgressIndicator();
        }

        internal static void UpdateStatusBarProgressIndicator()
        {
            if (_statusBar == null)
            {
                _statusBar = StatusBar.GetForCurrentView();
            }

            bool progressIndicatorIsVisible = false;
            bool progressIndicatorIsIndeterminate = false;
            string progressIndicatorText = string.Empty;
            double progressIndicatorValue = 0;

            ProgressIndicator progressIndicator = ProgressIndicator.GetActiveProgressIndicator();
            if (progressIndicator != null)
            {
                progressIndicatorIsVisible = progressIndicator.IsVisible;
                progressIndicatorIsIndeterminate = progressIndicator.IsIndeterminate;
                progressIndicatorText = progressIndicator.Text;
                progressIndicatorValue = progressIndicator.Value;
            }

            if (progressIndicatorIsVisible)
            {
                var asyncAction = _statusBar.ProgressIndicator.ShowAsync();
            }
            else
            {
                var asyncAction = _statusBar.ProgressIndicator.HideAsync();
            }

            if (progressIndicatorIsIndeterminate)
            {
                _statusBar.ProgressIndicator.ProgressValue = null;
            }
            else
            {
                _statusBar.ProgressIndicator.ProgressValue = progressIndicatorValue;
            }

            _statusBar.ProgressIndicator.Text = progressIndicatorText;
        }

        internal static Page GetActivePage()
        {
            Page activePage = TryGetActivePage();
            if (activePage == null)
            {
                throw new InvalidOperationException();
            }
            return activePage;
        }

        internal static Page TryGetActivePage()
        {
            var frame = Frame;
            if (frame != null)
            {
                return frame.Content as Page;
            }
            return null;
        }

        private static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            Window.Current.Activated += OnWindowActivated;

            _initialized = true;
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

            if (object.ReferenceEquals(d, TryGetActivePage()))
            {
                UpdateStatusBar();
            }
        }

        private static void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            UpdateStatusBar();
        }

        private static void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            UpdateStatusBar();
        }
    }
}
