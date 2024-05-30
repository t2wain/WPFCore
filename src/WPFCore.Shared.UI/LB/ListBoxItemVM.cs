using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFCore.Shared.UI.LB
{
    public partial class ListBoxItemVM : ObservableObject
    {

        public string ID { get; set; }

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private bool _isSelected = false;

        public ListBoxVM Parent { get; set; }

    }
}
