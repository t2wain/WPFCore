using System.Windows.Controls;

namespace WPFCore.ElectGrid.DG
{
    /// <summary>
    /// Interaction logic for UDataGridView.xaml
    /// </summary>
    public partial class UDataGridView : UserControl
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

    }
}
