using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
{
    public class MenuItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MenuItemTemplate { get; set; } = null!;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is MenuItemVM vm)
            {
                return vm.MenuType switch
                {
                    MenuTypeEnum.Separator => base.SelectTemplate(item, container),
                    _ => MenuItemTemplate
                };
            }
            return base.SelectTemplate(item, container);
        }
    }
}
