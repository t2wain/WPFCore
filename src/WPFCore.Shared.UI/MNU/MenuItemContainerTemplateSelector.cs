using System.Windows;
using System.Windows.Controls;

namespace WPFCore.Shared.UI.MNU
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
