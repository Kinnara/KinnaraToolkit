using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kinnara.Xaml.Controls
{
    public static partial class ContextMenuService
    {
        /// <summary>
        /// Handles changes to the ContextMenu DependencyProperty.
        /// </summary>
        /// <param name="o">DependencyObject that changed.</param>
        /// <param name="e">Event data for the DependencyPropertyChangedEvent.</param>
        private static void OnContextMenuChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = o as FrameworkElement;
            if (null != element)
            {
                MenuFlyout oldContextMenu = e.OldValue as MenuFlyout;
                if (null != oldContextMenu)
                {
                    var holdListener = GetHoldListener(element);
                    holdListener.HoldingStarted -= OnHoldingStarted;
                    holdListener.HoldingEnded -= OnHoldingEnded;
                    holdListener.Dispose();
                    element.ClearValue(HoldListenerProperty);
                    element.ClearValue(ReleaseThresholdProperty);
                }
                MenuFlyout newContextMenu = e.NewValue as MenuFlyout;
                if (null != newContextMenu)
                {
                    var holdListener = new HoldListener(element);
                    holdListener.HoldingStarted += OnHoldingStarted;
                    holdListener.HoldingEnded += OnHoldingEnded;
                    SetHoldListener(element, holdListener);
                }
            }
        }

        private static void OnHoldingStarted(object sender, EventArgs e)
        {
            var holdListener = (HoldListener)sender;
            var element = holdListener.Target as FrameworkElement;
            if (element != null)
            {
                MenuFlyout contextMenu = GetContextMenu(element);
                if (contextMenu != null && GetIsEnabled(element))
                {
                    SetReleaseThreshold(contextMenu, DateTimeOffset.UtcNow.AddSeconds(0.3));
                    contextMenu.ShowAt(element);
                }
            }
        }

        private static void OnHoldingEnded(object sender, EventArgs e)
        {
            var holdListener = (HoldListener)sender;
            var element = holdListener.Target as FrameworkElement;
            if (element != null)
            {
                MenuFlyout contextMenu = GetContextMenu(element);
                if (contextMenu != null && DateTimeOffset.UtcNow <= GetReleaseThreshold(contextMenu))
                {
                    contextMenu.Hide();
                }

                element.ClearValue(ReleaseThresholdProperty);
            }
        }

        #region HoldListener

        private static HoldListener GetHoldListener(UIElement element)
        {
            return (HoldListener)element.GetValue(HoldListenerProperty);
        }

        private static void SetHoldListener(UIElement element, HoldListener value)
        {
            element.SetValue(HoldListenerProperty, value);
        }

        private static readonly DependencyProperty HoldListenerProperty = DependencyProperty.RegisterAttached(
            "HoldListener",
            typeof(HoldListener),
            typeof(ContextMenuService),
            null);

        #endregion

        #region ReleaseThreshold

        private static DateTimeOffset GetReleaseThreshold(MenuFlyout element)
        {
            return (DateTimeOffset)element.GetValue(ReleaseThresholdProperty);
        }

        private static void SetReleaseThreshold(MenuFlyout element, DateTimeOffset value)
        {
            element.SetValue(ReleaseThresholdProperty, value);
        }

        private static readonly DependencyProperty ReleaseThresholdProperty = DependencyProperty.RegisterAttached(
            "ReleaseThreshold",
            typeof(DateTimeOffset),
            typeof(ContextMenuService),
            new PropertyMetadata(DateTimeOffset.MinValue));

        #endregion
    }
}
