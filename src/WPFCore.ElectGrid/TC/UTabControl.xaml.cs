using System.Windows.Controls;

namespace WPFCore.ElectGrid.TC
{
    /// <summary>
    /// Interaction logic for UTabControl.xaml
    /// </summary>
    public partial class UTabControl : UserControl
    {
        public UTabControl()
        {
            InitializeComponent();
        }

        public TabControl TControl => this._utc;

        UTabControlBinder _tcb = null!;
        public void Init(IServiceProvider provider)
        {
            _tcb = new UTabControlBinder();
            _tcb.Init(this, provider);
        }

        public TabItem SelectedItem => this.TControl.SelectedItem as TabItem;
    }
}
