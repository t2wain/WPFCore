using System.Windows.Controls;

namespace WPFCore.ElectGrid.DG
{
    /// <summary>
    /// Interaction logic for UDataGridView.xaml
    /// </summary>
    public partial class UDataGridView : UserControl, IDisposable
    {
        public UDataGridView()
        {
            InitializeComponent();
        }

        bool _isInit = false;
        private DGBinder _dgvBind = null!;
        public void Init(DGridVM vm)
        {
            if (!_isInit)
            {
                this.DataContext = vm;
                this._dgvBind = new DGBinder();
                this._dgvBind.InitDataGrid(_dgData, vm);
                _isInit = true;
            }
        }

        internal void ShowEditWindow()
        {
            _dgvBind.ShowEditWindow();
        }

        internal void ShowFilterWindow()
        {
            _dgvBind.ShowFilterWindow();
        }

        internal void ClearFilter()
        {
            _dgvBind.ClearFilter();
        }

        public void Dispose()
        {
            this._dgvBind?.Dispose();
            this._dgvBind = null!;
        }
    }
}
