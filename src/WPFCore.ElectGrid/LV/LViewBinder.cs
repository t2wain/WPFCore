using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public class LViewBinder : ListViewBinder
    {
        /// <summary>
        /// When report data changed, recreate the GridView
        /// </summary>
        protected override void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e)
        {
            base.ListenPropertyChangedOnVM(sender, e);

            var vm2 = this.VM as LViewVM;
            switch (e.PropertyName)
            {
                case nameof(vm2.ReportDef):
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
                case LViewEnum.ReportDef:
                    vw = GridConfig.CreateGeneralReport(this.ListVM.ReportDef!);
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
