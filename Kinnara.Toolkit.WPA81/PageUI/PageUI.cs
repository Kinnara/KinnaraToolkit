using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Represents the UI of a page.
    /// </summary>
    [TemplatePart(Name = ElementTitlePanelName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementPortraitTitlePanelHostName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = ElementLandscapeTitlePanelHostName, Type = typeof(FrameworkElement))]
    [TemplateVisualState(GroupName = GroupOrientation, Name = StatePortrait)]
    [TemplateVisualState(GroupName = GroupOrientation, Name = StateLandscape)]
    public class PageUI : ContentControl
    {
        private const string GroupOrientation = "OrientationStates";
        private const string StatePortrait = "Portrait";
        private const string StateLandscape = "Landscape";

        private const string ElementTitlePanelName = "TitlePanel";
        private const string ElementPortraitTitlePanelHostName = "PortraitTitlePanelHost";
        private const string ElementLandscapeTitlePanelHostName = "LandscapeTitlePanelHost";

        /// <summary>
        /// Initializes a new instance of the PageUI class.
        /// </summary>
        public PageUI()
        {
            DefaultStyleKey = typeof(PageUI);

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        #region Title

        /// <summary>
        /// Gets or sets the content for the page title.
        /// </summary>
        /// 
        /// <returns>
        /// The content of the page title. The default is null.
        /// </returns>
        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Identifies the Title dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(object),
            typeof(PageUI),
            null);

        #endregion

        #region TitleTemplate

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the page title.
        /// </summary>
        /// 
        /// <returns>
        /// The template that specifies the visualization of the title object. The default is null.
        /// </returns>
        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the TitleTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleTemplateProperty = DependencyProperty.Register(
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(PageUI),
            new PropertyMetadata(null));

        #endregion

        #region TitleVisibility

        /// <summary>
        /// Gets or sets the visibility of the page title.
        /// </summary>
        /// 
        /// <returns>
        /// A value of the enumeration. The default value is Visible.
        /// </returns>
        public Visibility TitleVisibility
        {
            get { return (Visibility)GetValue(TitleVisibilityProperty); }
            set { SetValue(TitleVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifies the TitleVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleVisibilityProperty = DependencyProperty.Register(
            "TitleVisibility",
            typeof(Visibility),
            typeof(PageUI),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region TitleContinuumEnabled

        /// <summary>
        /// Gets or sets a Boolean value indicating if the page title is the entrance element for the continuum navigation.
        /// </summary>
        /// 
        /// <returns>
        /// Returns true if the page title is the entrance element; otherwise false. The default value is false.
        /// </returns>
        public bool TitleContinuumEnabled
        {
            get { return (bool)GetValue(TitleContinuumEnabledProperty); }
            set { SetValue(TitleContinuumEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the TitleContinuumEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleContinuumEnabledProperty = DependencyProperty.Register(
            "TitleContinuumEnabled",
            typeof(bool),
            typeof(PageUI),
            null);

        #endregion

        #region Subtitle

        /// <summary>
        /// Gets or sets the content for the page subtitle.
        /// </summary>
        /// 
        /// <returns>
        /// The content of the page subtitle. The default is null.
        /// </returns>
        public object Subtitle
        {
            get { return (object)GetValue(SubtitleProperty); }
            set { SetValue(SubtitleProperty, value); }
        }

        /// <summary>
        /// Identifies the Subtitle dependency property.
        /// </summary>
        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register(
            "Subtitle",
            typeof(object),
            typeof(PageUI),
            null);

        #endregion

        #region SubtitleTemplate

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the page subtitle.
        /// </summary>
        /// 
        /// <returns>
        /// The template that specifies the visualization of the subtitle object. The default is null.
        /// </returns>
        public DataTemplate SubtitleTemplate
        {
            get { return (DataTemplate)GetValue(SubtitleTemplateProperty); }
            set { SetValue(SubtitleTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the SubtitleTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty SubtitleTemplateProperty = DependencyProperty.Register(
            "SubtitleTemplate",
            typeof(DataTemplate),
            typeof(PageUI),
            new PropertyMetadata(null));

        #endregion

        #region SubtitleVisibility

        /// <summary>
        /// Gets or sets the visibility of the page subtitle.
        /// </summary>
        /// 
        /// <returns>
        /// A value of the enumeration. The default value is Collapsed.
        /// </returns>
        public Visibility SubtitleVisibility
        {
            get { return (Visibility)GetValue(SubtitleVisibilityProperty); }
            set { SetValue(SubtitleVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifies the SubtitleVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty SubtitleVisibilityProperty = DependencyProperty.Register(
            "SubtitleVisibility",
            typeof(Visibility),
            typeof(PageUI),
            new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #region SubtitleContinuumEnabled

        /// <summary>
        /// Gets or sets a Boolean value indicating if the page subtitle is the entrance element for the continuum navigation.
        /// </summary>
        /// 
        /// <returns>
        /// Returns true if the page subtitle is the entrance element; otherwise false. The default value is false.
        /// </returns>
        public bool SubtitleContinuumEnabled
        {
            get { return (bool)GetValue(SubtitleContinuumEnabledProperty); }
            set { SetValue(SubtitleContinuumEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the SubtitleContinuumEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty SubtitleContinuumEnabledProperty = DependencyProperty.Register(
            "SubtitleContinuumEnabled",
            typeof(bool),
            typeof(PageUI),
            null);

        #endregion

        #region Header

        /// <summary>
        /// Gets or sets the content for the page header.
        /// </summary>
        /// 
        /// <returns>
        /// The content of the page header. The default is null.
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
            typeof(PageUI),
            null);

        #endregion

        #region HeaderTemplate

        /// <summary>
        /// Gets or sets the DataTemplate used to display the content of the page header.
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
            typeof(PageUI),
            new PropertyMetadata(null));

        #endregion

        #region HeaderVisibility

        /// <summary>
        /// Gets or sets the visibility of the page header.
        /// </summary>
        /// 
        /// <returns>
        /// A value of the enumeration. The default value is Visible.
        /// </returns>
        public Visibility HeaderVisibility
        {
            get { return (Visibility)GetValue(HeaderVisibilityProperty); }
            set { SetValue(HeaderVisibilityProperty, value); }
        }

        /// <summary>
        /// Identifies the HeaderVisibility dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderVisibilityProperty = DependencyProperty.Register(
            "HeaderVisibility",
            typeof(Visibility),
            typeof(PageUI),
            new PropertyMetadata(Visibility.Visible));

        #endregion

        #region HeaderContinuumEnabled

        /// <summary>
        /// Gets or sets a Boolean value indicating if the page header is the entrance element for the continuum navigation.
        /// </summary>
        /// 
        /// <returns>
        /// Returns true if the page header is the entrance element; otherwise false. The default value is false.
        /// </returns>
        public bool HeaderContinuumEnabled
        {
            get { return (bool)GetValue(HeaderContinuumEnabledProperty); }
            set { SetValue(HeaderContinuumEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the HeaderContinuumEnabled dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderContinuumEnabledProperty = DependencyProperty.Register(
            "HeaderContinuumEnabled",
            typeof(bool),
            typeof(PageUI),
            null);

        #endregion

        #region TitlePanelMargin

        /// <summary>
        /// Gets or sets the outer margin of the title panel.
        /// </summary>
        /// 
        /// <returns>
        /// Provides margin values for the title panel.
        /// </returns>
        public Thickness TitlePanelMargin
        {
            get { return (Thickness)GetValue(TitlePanelMarginProperty); }
            set { SetValue(TitlePanelMarginProperty, value); }
        }

        /// <summary>
        /// Identifies the TitlePanelMargin dependency property.
        /// </summary>
        public static readonly DependencyProperty TitlePanelMarginProperty = DependencyProperty.Register(
            "TitlePanelMargin",
            typeof(Thickness),
            typeof(PageUI),
            null);

        #endregion

        #region LandscapeTitlePanelHost

        /// <summary>
        /// Gets or sets the Border used to host the title panel in landscape mode.
        /// </summary>
        /// 
        /// <returns>
        /// The Border used to host the title panel in landscape mode. The default value is null.
        /// </returns>
        public Border LandscapeTitlePanelHost
        {
            get { return (Border)GetValue(LandscapeTitlePanelHostProperty); }
            set { SetValue(LandscapeTitlePanelHostProperty, value); }
        }

        /// <summary>
        /// Identifies the LandscapeTitlePanelHost dependency property.
        /// </summary>
        public static readonly DependencyProperty LandscapeTitlePanelHostProperty = DependencyProperty.Register(
            "LandscapeTitlePanelHost",
            typeof(Border),
            typeof(PageUI),
            new PropertyMetadata(null, (d, e) => ((PageUI)d).OnLandscapeTitlePanelHostChanged(e)));

        private void OnLandscapeTitlePanelHostChanged(DependencyPropertyChangedEventArgs e)
        {
            UpdateActualLandscapeTitlePanelHost();
        }

        #endregion

        #region ActualLandscapeTitlePanelHost

        private Border ActualLandscapeTitlePanelHost
        {
            get { return (Border)GetValue(ActualLandscapeTitlePanelHostProperty); }
            set { SetValue(ActualLandscapeTitlePanelHostProperty, value); }
        }

        private static readonly DependencyProperty ActualLandscapeTitlePanelHostProperty = DependencyProperty.Register(
            "ActualLandscapeTitlePanelHost",
            typeof(Border),
            typeof(PageUI),
            new PropertyMetadata(null, (d, e) => ((PageUI)d).OnActualLandscapeTitlePanelHostChanged(e)));

        private void OnActualLandscapeTitlePanelHostChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldHost = (Border)e.OldValue;
            if (oldHost != null && oldHost.Child != null)
            {
                oldHost.Child = null;
            }

            UpdateTitlePanelPlacement();
        }

        private void UpdateActualLandscapeTitlePanelHost()
        {
            ActualLandscapeTitlePanelHost = LandscapeTitlePanelHost ?? ElementLandscapeTitlePanelHost;
        }

        #endregion

        private FrameworkElement ElementTitlePanel { get; set; }

        private Border ElementPortraitTitlePanelHost { get; set; }

        private Border ElementLandscapeTitlePanelHost { get; set; }

        private bool IsLandscape
        {
            get
            {
                if (DesignMode.DesignModeEnabled)
                {
                    var bounds = Window.Current.Bounds;
                    return bounds.Width > bounds.Height;
                }
                else
                {
                    return ApplicationView.GetForCurrentView().Orientation == ApplicationViewOrientation.Landscape;
                }
            }
        }

        /// <summary>
        /// Builds the visual tree for the control when a new template is applied.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ElementTitlePanel = GetTemplateChild(ElementTitlePanelName) as FrameworkElement;
            ElementPortraitTitlePanelHost = GetTemplateChild(ElementPortraitTitlePanelHostName) as Border;
            ElementLandscapeTitlePanelHost = GetTemplateChild(ElementLandscapeTitlePanelHostName) as Border;

            UpdateActualLandscapeTitlePanelHost();
            UpdateVisualStates(false);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += OnWindowSizeChanged;

            UpdateTitlePanelPlacement();
            UpdateVisualStates(false);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= OnWindowSizeChanged;
        }

        private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            UpdateTitlePanelPlacement();
            UpdateVisualStates(true);
        }

        private void UpdateTitlePanelPlacement()
        {
            var titlePanel = ElementTitlePanel;
            var portraitTitlePanelHost = ElementPortraitTitlePanelHost;
            var landscapeTitlePanelHost = ActualLandscapeTitlePanelHost;

            if (titlePanel != null && portraitTitlePanelHost != null && landscapeTitlePanelHost != null)
            {
                if (IsLandscape)
                {
                    portraitTitlePanelHost.Child = null;
                    landscapeTitlePanelHost.Child = titlePanel;
                }
                else
                {
                    landscapeTitlePanelHost.Child = null;
                    portraitTitlePanelHost.Child = titlePanel;
                }
            }
        }

        private void UpdateVisualStates(bool useTransitions)
        {
            VisualStateManager.GoToState(this, IsLandscape ? StateLandscape : StatePortrait, useTransitions);
        }
    }
}
