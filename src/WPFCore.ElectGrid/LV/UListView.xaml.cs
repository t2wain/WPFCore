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
        }

        private LViewBinder? _lvwBind;
        public void Init(LViewVM vm)
        {
            this._lvwBind = new LViewBinder();
            this._lvwBind.ListViewControl = this._lvwData;
            this._lvwData.DataContext = vm;
        }
    }
}
