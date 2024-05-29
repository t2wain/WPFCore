using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPFCore.ElectIndex.TV;

namespace WPFCore.App2
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
            }
            //this.Loaded += OnLoaded;
        }

        protected void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current is App app)
            {
                var vm = app.Provider.GetRequiredService<UTreeLBoxVM>();
                this._tvw.Init(vm);
            }
        }
    }
}