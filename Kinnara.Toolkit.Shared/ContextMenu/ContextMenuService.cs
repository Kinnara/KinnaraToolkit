using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kinnara.Xaml.Controls
{
    /// <summary>
    /// Provides the system implementation for displaying a context meuu.
    /// </summary>
    public static partial class ContextMenuService
    {
        /// <summary>
        /// Gets the value of the ContextMenu property of the specified object.
        /// </summary>
        /// <param name="element">Object to query concerning the ContextMenu property.</param>
        /// <returns>Value of the ContextMenu property.</returns>
        public static MenuFlyout GetContextMenu(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (MenuFlyout)element.GetValue(ContextMenuProperty);
        }

        /// <summary>
        /// Sets the value of the ContextMenu property of the specified object.
        /// </summary>
        /// <param name="element">Object to set the property on.</param>
        /// <param name="value">Value to set.</param>
        public static void SetContextMenu(DependencyObject element, MenuFlyout value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(ContextMenuProperty, value);
        }

        /// <summary>
        /// Identifies the ContextMenu attached property.
        /// </summary>
        public static readonly DependencyProperty ContextMenuProperty = DependencyProperty.RegisterAttached(
            "MenuFlyout",
            typeof(MenuFlyout),
            typeof(ContextMenuService),
            new PropertyMetadata(null, OnContextMenuChanged));

        /// <summary>
        /// Gets the value of the IsEnabled property of the specified object.
        /// </summary>
        /// <param name="element">Object to query concerning the IsEnabled property.</param>
        /// <returns>Value of the IsEnabled property.</returns>
        public static bool GetIsEnabled(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            return (bool)element.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Sets the value of the IsEnabled property of the specified object.
        /// </summary>
        /// <param name="element">Object to set the property on.</param>
        /// <param name="value">Value to set.</param>
        public static void SetIsEnabled(DependencyObject element, bool value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            element.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// Identifies the IsEnabled attached property.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(ContextMenuService),
            new PropertyMetadata(true));
    }
}
