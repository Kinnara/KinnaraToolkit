using KinnaraToolkitSample.Data;
using Windows.UI.Xaml.Controls;

namespace KinnaraToolkitSample
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SamplesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (SampleItem)e.ClickedItem;
            Frame.Navigate(item.SourcePageType, null, item.NavigationTransitionInfo);
        }
    }
}
