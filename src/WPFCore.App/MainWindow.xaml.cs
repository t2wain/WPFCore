using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPFCore.ElectIndex.TV;

namespace WPFCore.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }

        AppMainBinder _main = null!;

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            _main = new AppMainBinder();
            _main.Init(this, _mainMenu, _sb);

            if (Application.Current is App app)
            {
                _main.InitElectGrid(_tcv, app.Provider);

                var tvm = app.Provider.GetRequiredService<UTreeLBoxVM>();
                _main.InitElectIndex(_tvw, tvm);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _main.Dispose();
            base.OnClosed(e);
        }


    }
}