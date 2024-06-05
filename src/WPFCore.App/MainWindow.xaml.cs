using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using WPFCore.ElectIndex.TV;
using WPFCore.Shared.UI.SB;

namespace WPFCore.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UTreeLBoxVM _vm = null!;
        private StatusBarVM _svm = null!;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {

            if (Application.Current is App app)
            {
                _svm = _sb.Init();
                _vm = app.Provider.GetRequiredService<UTreeLBoxVM>();
                _vm.PropertyChanged += OnPropertyChanged;
                this._tvw.Init(_vm);
                this._mainMenu.Init();
            }
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_vm.ItemCount):
                    _svm.LeftMessage = _vm.ItemCount > 0 ? $"Item count: {_vm.ItemCount}" : "";
                    break;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _vm.PropertyChanged -= OnPropertyChanged;
            this._tvw.Dispose();
            base.OnClosed(e);
        }
    }
}