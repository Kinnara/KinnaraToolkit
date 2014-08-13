using KinnaraToolkitSample.Data;
using KinnaraToolkitSample.PageUI;
using Windows.UI.Xaml.Controls;

namespace KinnaraToolkitSample
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SamplesList.ItemsSource = new[]
            {
                new SampleItem("Page UI", typeof(PageUISample)),
            };
        }

        private void SamplesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (SampleItem)e.ClickedItem;
            Frame.Navigate(item.SourcePageType, null, item.NavigationTransitionInfo);
        }
    }
}
