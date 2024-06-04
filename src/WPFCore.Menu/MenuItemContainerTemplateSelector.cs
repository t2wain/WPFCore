using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.MNU;

namespace WPFCore.Menu
{
    public class MenuItemContainerTemplateSelector : ItemContainerTemplateSelector
    {
        public DataTemplate MenuItemTemplate { get; set; } = null!;

        public DataTemplate SeparatorTemplate { get; set; } = null!;

        public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl)
        {
            if (item is MenuItemVM vm)
            {
                return vm.MenuType switch
                {
                    MenuTypeEnum.Separator => SeparatorTemplate,
                    _ => MenuItemTemplate
                };
            }
            return base.SelectTemplate(item, parentItemsControl);
        }
    }
}
