using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public class LViewBinder : ListViewBinder
    {
        protected override void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e)
        {
            base.ListenPropertyChangedOnVM(sender, e);
            switch (e.PropertyName)
            {
                case "ListItemsView":
                    this.SetupGridView();
                    this.ListVM.IsEnabled = true;
                    break;
            }
        }

        protected LViewVM ListVM => (this.VM as LViewVM)!;

        #region Setup GridView

        Style? _style = null;
        protected void SetupGridView()
        {
            GridView? vw = null;
            switch(this.ListVM.ViewType) 
            {
                case LViewEnum.Motors:
                    vw = GridConfig.BOMItemsReport;
                    break;
                case LViewEnum.OtherElectricalEquipment:
                    vw = GridConfig.BOMItemsReport;
                    break;
                case LViewEnum.Transformers:
                    vw = GridConfig.BOMItemsReport;
                    break;
            }

            this.ListViewControl!.View = vw;

            if (this._style == null)
            {
                this._style = GridUtility.CreateViewStyle();
                this.ListViewControl.ItemContainerStyle = this._style;
            }

        }

        #endregion
    }
}
