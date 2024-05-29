using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFCore.Shared.UI.LB
{
    public class ListBoxItemVM : ObservableObject
    {

        public string ID { get; set; }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set { SetProperty<string>(ref this._name, value, "Name"); }
        }


        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return this._isSelected; }
            set { SetProperty<bool>(ref this._isSelected, value, "IsSelected"); }
        }

        public ListBoxVM Parent { get; set; }

    }
}
