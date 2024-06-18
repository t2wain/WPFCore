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
            this.Loaded += this.OnLoaded;
            this.Unloaded += this.OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is LViewVM vm)
            {
                if (!_isInit)
                {
                    this._lvwBind = new LViewBinder();
                    this._lvwBind.InitListView(_lvwData, vm);
                    _isInit = true;
                }
            }
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

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this._lvwBind?.Dispose();
        }
    }
}
