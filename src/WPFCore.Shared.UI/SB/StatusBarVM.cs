using CommunityToolkit.Mvvm.ComponentModel;

namespace WPFCore.Shared.UI.SB
{
    public partial class StatusBarVM : ObservableRecipient
    {
        private string _leftMessage = "Ready";
        public string LeftMessage
        {
            get => _leftMessage;
            set
            {
                string msg = "Ready";
                if (!string.IsNullOrWhiteSpace(value))
                {
                    msg = value;
                }
                SetProperty(ref _leftMessage, msg);
            }
        }


        [ObservableProperty] 
        private string? _rightMessage;
    }
}
