using System;
using Windows.UI.Xaml.Media.Animation;

namespace KinnaraToolkitSample.Data
{
    public class SampleItem
    {
        public SampleItem(string displayName, Type sourcePageType)
            : this(displayName, sourcePageType, null)
        {
        }

        public SampleItem(string displayName, Type sourcePageType, NavigationTransitionInfo navigationTransitionInfo)
        {
            DisplayName = displayName;
            SourcePageType = sourcePageType;
            NavigationTransitionInfo = navigationTransitionInfo;
        }

        public string DisplayName { get; private set; }
        public Type SourcePageType { get; private set; }
        public NavigationTransitionInfo NavigationTransitionInfo { get; private set; }
    }
}
