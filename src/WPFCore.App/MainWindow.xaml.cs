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
            if (Application.Current is App app)
            {
                var vm = app.Provider.GetRequiredService<UTreeLBoxVM>();
                this._tvw.Init(vm);
                this._mainMenu.Init();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            this._tvw.Dispose();
            base.OnClosed(e);
        }
    }
}