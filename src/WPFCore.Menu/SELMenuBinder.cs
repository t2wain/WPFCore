using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.MNU;

namespace WPFCore.Menu
{
    public class SELMenuBinder : MenuBinder
    {
        private readonly MNRepo _repo;

        public SELMenuBinder(MNRepo repo)
        {
            this._repo = repo;
        }

        protected override void OnSubmenOpened(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is MenuItem m 
                && m.DataContext is SELMenuItemVM vm 
                && vm.IsChildrenNotPopulated)
            {
                vm.Poplulate(_repo.GetSubMenu(vm.MenuCommandType));
            }
        }
    }
}
