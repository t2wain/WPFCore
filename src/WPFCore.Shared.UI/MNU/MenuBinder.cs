using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
{
    public class MenuBinder : IDisposable
    {
        private Menu? _mnu;
        public Menu? MenuControl
        {
            set
            {
                this._mnu = value;
                if (this._mnu != null && !DesignerProperties.GetIsInDesignMode(this._mnu))
                    this.InitMenu(this._mnu);
            }
            protected get
            {
                return this._mnu;
            }
        }

        RoutedEventHandler _h1 = null!;

        virtual protected void InitMenu(Menu mnu)
        {
            // Configure handlers for TreeViewItem events
            _h1 = new RoutedEventHandler(this.OnSubmenOpened);
            mnu.AddHandler(MenuItem.SubmenuOpenedEvent, _h1);
        }

        protected virtual void OnSubmenOpened(object sender, RoutedEventArgs e) { }

        public void Dispose()
        {
            this.MenuControl?.RemoveHandler(MenuItem.SubmenuOpenedEvent, _h1);
            this.MenuControl = null;
        }
    }
}
