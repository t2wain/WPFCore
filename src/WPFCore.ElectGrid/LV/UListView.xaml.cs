using System.Windows;
using System.Windows.Controls;

namespace WPFCore.ElectGrid.LV
{
    /// <summary>
    /// Interaction logic for UListView.xaml
    /// </summary>
    public partial class UListView : UserControl
    {
        public UListView()
        {
            InitializeComponent();
            this.Unloaded += this.OnUnloaded;
        }

        private LViewVM? _vm;
        private LViewBinder? _lvwBind;

        public void Init(LViewVM vm)
        {
            this._vm = vm;
            this._lvwBind = new LViewBinder();
            this._lvwBind.InitListView(_lvwData, vm);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this._lvwBind?.Dispose();
        }
    }
}
