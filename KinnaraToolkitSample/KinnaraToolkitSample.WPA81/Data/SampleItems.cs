using KinnaraToolkitSample.DateTimePickers;
using KinnaraToolkitSample.PageUI;
using System.Collections.ObjectModel;

namespace KinnaraToolkitSample.Data
{
    public class SampleItems : ObservableCollection<SampleItem>
    {
        public SampleItems()
        {
            Add(new SampleItem("Page UI", typeof(PageUISample)));
            Add(new SampleItem("Custom Date Picker", typeof(DatePickerSample)));
            Add(new SampleItem("Custom Time Picker", typeof(TimePickerSample)));
        }
    }
}
