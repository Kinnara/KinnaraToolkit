using KinnaraToolkitSample.Data;
using Windows.UI.Xaml.Controls;

namespace KinnaraToolkitSample.PageUI
{
    public sealed partial class PageUISample
    {
        public PageUISample()
        {
            this.InitializeComponent();

            SamplesList.ItemsSource = new[]
            {
                new SampleItem("titled page", typeof(TitledPage)),
                new SampleItem("subtitled page", typeof(SubtitledPage)),
                new SampleItem("single pivot page", typeof(SinglePivotPage)),
                new SampleItem("subtitled single pivot page", typeof(SubtitledSinglePivotPage)),
            };
        }

        private void SamplesList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (SampleItem)e.ClickedItem;
            Frame.Navigate(item.SourcePageType, null, item.NavigationTransitionInfo);
        }
    }
}
