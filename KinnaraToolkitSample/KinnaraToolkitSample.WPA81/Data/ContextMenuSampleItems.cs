using System.Collections.ObjectModel;

namespace KinnaraToolkitSample.Data
{
    public class ContextMenuSampleItems : ObservableCollection<ContextMenuSampleItem>
    {
        public ContextMenuSampleItems()
        {
            Add(new ContextMenuSampleItem { Title = "Context menu enabled", IsContextMenuEnabled = true });
            Add(new ContextMenuSampleItem { Title = "Context menu disabled" });
        }
    }
}
