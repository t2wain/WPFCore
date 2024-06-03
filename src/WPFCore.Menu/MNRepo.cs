using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFCore.Shared.UI.MNU;
using CT = WPFCore.Menu.MenuCommandEnum;
using MT = WPFCore.Shared.UI.MNU.MenuTypeEnum;

namespace WPFCore.Menu
{
    public class MNRepo
    {
        #region Main

        public List<INotifyPropertyChanged> GetMain() =>
            new List<INotifyPropertyChanged>() 
            {
                CreateSubMenu("File", CT.File),
                CreateSubMenu("Edit", CT.Edit),
                CreateSubMenu("View", CT.View),
                CreateSubMenu("Actions", CT.Actions),
                CreateSubMenu("Tools", CT.Tools),
                CreateSubMenu("SmartPlant", CT.SmartPlant),
                CreateSubMenu("Reports", CT.Reports),
                CreateSubMenu("Window", CT.Window),
                CreateSubMenu("Help", CT.Help),
            };

        #endregion

        public List<INotifyPropertyChanged> GetSubMenu(CT parentCmd)
        {
            return parentCmd switch
            {
                CT.File => GetFileSubMenu(),
                CT.Edit => GetEditSubMenu(),
                CT.View => GetViewSubMenu(),
                CT.Display => GetViewDisplaySubMenu(),
                _ => []
            };
        }

        protected List<INotifyPropertyChanged> GetFileSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("New", CT.New),
                CreateMenuItem("Open", CT.Open),
                CreateMenuItem("Close", CT.Close),
                CreateMenuItem("Save As...", CT.SaveAs),
                CreateMenuItem("Tools", CT.Tools),
                CreateMenuItem("Preferences...", CT.Preferences),  
                CreateMenuItem("Exit", CT.Exit),
            };

        protected List<INotifyPropertyChanged> GetEditSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Cut", CT.Cut),
                CreateMenuItem("Copy", CT.Copy),
                CreateMenuItem("Paste", CT.Paste),
                CreateMenuItem("Delete", CT.Delete),
                CreateMenuItem("Select All", CT.SelectAll),
                CreateMenuItem("Duplicate", CT.Duplicate),
                CreateMenuItem("Rename", CT.Rename),
                CreateMenuItem("Document Properties...", CT.DocumentProperties),
                CreateMenuItem("Common Properties...", CT.CommonProperties),
                CreateMenuItem("Transformer Connection And Tapping", CT.TransformerConnectionAndTapping),
            };

        protected List<INotifyPropertyChanged> GetViewSubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Refresh", CT.Refresh),
                CreateMenuItem("Show Selected Electrical Branch Only", CT.ShowSelectedElectricalBranchOnly),
                CreateMenuItem("Show In New Window", CT.ShowInNewWindow),
                CreateMenuItem("Show Items of All Plant Groups", CT.ShowItemsOfAllPlantGroups),
                CreateMenuItem("Show Related Items", CT.ShowRelatedItems),
                CreateSubMenu("Display", CT.Display),
                CreateMenuItem("Toolbars", CT.Toolbars),
            };

        protected List<INotifyPropertyChanged> GetViewDisplaySubMenu() =>
            new List<INotifyPropertyChanged>()
            {
                CreateMenuItem("Properties Window", CT.PropertiesWindow),
                CreateMenuItem("Working Area", CT.WorkingArea),
                CreateMenuItem("Item Status In Project", CT.ItemStatusInProject),
                CreateMenuItem("Catalog Explorer Window", CT.CatalogExplorerWindow),
            };

        #region Utility

        static INotifyPropertyChanged CreateSubMenu(string name, CT cmdType) =>
            new SELMenuItemVM()
            {
                Name = name,
                MenuCommandType = cmdType,
                MenuType = MT.SubMenu,
                Children = new ObservableCollection<INotifyPropertyChanged> { CreateDummyMenu() }
            };


        static INotifyPropertyChanged CreateMenuItem(string name, CT cmdType) =>
            new SELMenuItemVM()
            {
                Name = name,
                MenuCommandType = cmdType,
                MenuType = MT.Menu,
            };

        static INotifyPropertyChanged CreateDummyMenu() =>
            new SELMenuItemVM
            {
                Name = "Loading....",
                MenuType = MT.Dummy,
            };

        static INotifyPropertyChanged CreateSeparator() =>
            new SELMenuItemVM
            {
                MenuType = MT.Separator,
            };

        #endregion

    }
}
