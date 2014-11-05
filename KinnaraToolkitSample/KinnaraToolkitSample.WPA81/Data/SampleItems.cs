using KinnaraToolkitSample.ContextMenu;
using KinnaraToolkitSample.DateTimePickers;
using KinnaraToolkitSample.PageUI;
using System.Collections.ObjectModel;

namespace KinnaraToolkitSample.Data
{
    public class SampleItems : ObservableCollection<SampleItem>
    {
        public SampleItems()
        {
            Add(new SampleItem("page ui", typeof(PageUISample)));
            Add(new SampleItem("date picker", typeof(DatePickerSample)));
            Add(new SampleItem("time picker", typeof(TimePickerSample)));
            Add(new SampleItem("context menu", typeof(ContextMenuSample)));
        }
    }
}
