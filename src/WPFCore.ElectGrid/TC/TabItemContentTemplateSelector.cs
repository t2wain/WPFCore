using System.Windows;
using System.Windows.Controls;

namespace WPFCore.ElectGrid.TC
{

    public class TabItemContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ReportGridDataTemplate { get; set; } = null!;

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TabItemVM vm)
            {
                return vm.ItemType switch
                {
                    TabItemEnum.Report when this.ReportGridDataTemplate != null => this.ReportGridDataTemplate,
                    _ => base.SelectTemplate(item, container)
                };
            }

            return base.SelectTemplate(item, container);
        }
    }
}
