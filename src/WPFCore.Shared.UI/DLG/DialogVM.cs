using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFCore.Shared.UI.DLG
{
    public partial class DialogVM : ObservableObject
    {
        public DialogVM()
        {
            this.ApplyCmd = new RelayCommand(() => {
                if (this._ctl != null)
                {
                    Window.GetWindow(this._ctl).DialogResult = true;
                    this._ctl = null;
                }
            });

            this.CancelCmd = new RelayCommand(() =>
            {
                if (this._ctl != null)
                {
                    Window.GetWindow(this._ctl).DialogResult = false;
                    this._ctl = null;
                }
            });
        }

        [ObservableProperty]
        RelayCommand? _applyCmd = null;

        [ObservableProperty]
        RelayCommand? _cancelCmd = null;

        UserControl? _ctl = null;
        public void Init(UserControl ctl)
        {
            this._ctl = ctl;
            ctl.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(this.OnKeyDownHandler));
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                this.ApplyCmd!.Execute(null);
            else if (e.Key == Key.Escape)
                this.CancelCmd!.Execute(null);
        }
    }
}
