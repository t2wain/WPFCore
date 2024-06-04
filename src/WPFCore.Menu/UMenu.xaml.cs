using System.Windows.Controls;

namespace WPFCore.Menu
{
    /// <summary>
    /// Interaction logic for UMenu.xaml
    /// </summary>
    public partial class UMenu : UserControl
    {
        public UMenu()
        {
            InitializeComponent();
        }

        public MNRepo? Repo { get; set; }

        public void Init()
        {
            Repo = new MNRepo();
            var binder = new SELMenuBinder(Repo);
            binder.MenuControl = this._mainMenu;
            this.DataContext = Repo.GetMain();
        }

    }
}
