using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
{
    public class MenuItemContainerStyleSelector : StyleSelector
    {
        public Style MenuItemStyle { get; set; } = null!;

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is MenuItemVM vm)
            {
                return vm.MenuType switch
                {
                    MenuTypeEnum.Separator => base.SelectStyle(item, container),
                    _ => MenuItemStyle
                };
            }

            return base.SelectStyle(item, container);
        }
    }
}
