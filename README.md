# Kinnara Toolkit

Provides a set of components to help create beautiful and consistent Windows Phone user interfaces and make common programming tasks easier.

**Supported Framework**  
Windows Phone 8.1 [Windows Runtime apps only]

<a href="https://www.nuget.org/packages/Kinnara.Toolkit/" target="_blank">Download on NuGet</a>

### Context Menu Service
Provides the system implementation for displaying a context menu.

```XML
<StackPanel x:Name="ContextMenuTarget"
            Background="Transparent">
    <k:ContextMenuService.ContextMenu>
        <MenuFlyout>
            <MenuFlyoutItem Text="reply all"/>
            <MenuFlyoutItem Text="forward"/>
            <MenuFlyoutItem Text="mark as unread"/>
            <MenuFlyoutItem Text="flag for follow up"/>
            <MenuFlyoutItem Text="move to"/>
        </MenuFlyout>
    </k:ContextMenuService.ContextMenu>
</StackPanel>
```

### Custom Date & Time Pickers
Supports **null** value, custom flyout title, custom format, and placeholder text.

![Custom Date & Time Pickers](https://raw.githubusercontent.com/Kinnara/Media/master/KinnaraToolkit/CustomDateTimePickers.png)

### Page UI
Makes it super easy to create page layouts that match the built-in experience.

![Page UI](https://raw.githubusercontent.com/Kinnara/Media/master/KinnaraToolkit/PageUI.Gallery.png)

### System Tray
Provides methods and properties for interacting with the system tray on a page.

```XML
<Page
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:k="using:Kinnara.Xaml.Controls"
    k:SystemTray.BackgroundColor="{ThemeResource SystemColorControlAccentColor}"
    k:SystemTray.ForegroundColor="{ThemeResource PhoneTextOverAccentColor}">
</Page>
```
