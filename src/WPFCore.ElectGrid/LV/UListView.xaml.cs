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
        }

        private LViewBinder? _lvwBind;
        protected void OnLoaded(object sender, RoutedEventArgs e)
        {
            this._lvwBind = new LViewBinder();
            this._lvwBind.ListViewControl = this._lvwData;

            if (this.DataContext is UListViewVM ctx)
            {
                ctx.Init();
            }
        }
    }
}
