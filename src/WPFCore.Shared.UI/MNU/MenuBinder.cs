using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
{
    public class MenuBinder
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

        virtual protected void InitMenu(Menu mnu)
        {
            // Configure handlers for TreeViewItem events
            mnu.AddHandler(MenuItem.SubmenuOpenedEvent, new RoutedEventHandler(this.OnSubmenOpened));
        }

        protected virtual void OnSubmenOpened(object sender, RoutedEventArgs e) { }
    }
}
