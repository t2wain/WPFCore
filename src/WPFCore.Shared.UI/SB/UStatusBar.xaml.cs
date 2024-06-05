using System.Windows.Controls;

namespace WPFCore.Shared.UI.SB
{
    /// <summary>
    /// Interaction logic for UStatusBar.xaml
    /// </summary>
    public partial class UStatusBar : UserControl
    {
        public UStatusBar()
        {
            InitializeComponent();
        }

        public StatusBarVM Init()
        {
            var vm = new StatusBarVM();
            this._sb.DataContext = vm;
            return vm;
        }
    }
}
