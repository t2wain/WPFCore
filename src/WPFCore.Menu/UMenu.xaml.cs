using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Menu
{
    /// <summary>
    /// Interaction logic for UMenu.xaml
    /// </summary>
    public partial class UMenu : UserControl
    {
        private SELMenuBinder? _binder;

        public UMenu()
        {
            InitializeComponent();
            this.Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _binder?.Dispose();
            _binder = null;
        }

        public MNRepo? Repo { get; set; }

        public void Init()
        {
            Repo = new MNRepo();
            _binder = new SELMenuBinder(Repo);
            _binder.MenuControl = this._mainMenu;
            this.DataContext = Repo.GetMain();
        }

    }
}
