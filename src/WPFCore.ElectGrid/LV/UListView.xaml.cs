using System.Windows;
using System.Windows.Controls;

namespace WPFCore.ElectGrid.LV
{
    /// <summary>
    /// Interaction logic for UListView.xaml
    /// </summary>
    public partial class UListView : UserControl, IDisposable
    {
        public UListView()
        {
            InitializeComponent();
        }

        bool _isInit = false;
        private LViewBinder _lvwBind = null!;
        public void Init(LViewVM vm)
        {
            if (!_isInit)
            {
                this.DataContext = vm;
                this._lvwBind = new LViewBinder();
                this._lvwBind.InitListView(_lvwData, vm);
                _isInit = true;
            }
        }

        public void Dispose()
        {
            this._lvwBind?.Dispose();
            this._lvwBind = null!;
        }
    }
}
