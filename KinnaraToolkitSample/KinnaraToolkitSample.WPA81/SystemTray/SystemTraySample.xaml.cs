using Windows.UI.Xaml;

namespace KinnaraToolkitSample
{
    public sealed partial class SystemTraySample
    {
        public SystemTraySample()
        {
            InitializeComponent();
            UpdateButtons();
        }

        private void ShowProgressIndicator_Click(object sender, RoutedEventArgs e)
        {
            LoadingProgressIndicator.IsVisible = true;
            UpdateButtons();
        }

        private void HideProgressIndicator_Click(object sender, RoutedEventArgs e)
        {
            LoadingProgressIndicator.IsVisible = false;
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            bool isLoading = LoadingProgressIndicator.IsVisible;
            ShowProgressIndicator.IsEnabled = !isLoading;
            HideProgressIndicator.IsEnabled = isLoading;
        }
    }
}
