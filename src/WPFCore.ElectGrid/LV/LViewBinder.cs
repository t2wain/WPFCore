using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public class LViewBinder : ListViewBinder
    {


        public override void InitListView(ListView lv, ListViewVM vm)
        {
            base.InitListView(lv, vm);

            lv.GotFocus += this.OnFocus;
            lv.LostFocus += this.OnLostFocus;

        }

        protected LViewVM ListVM => (this.VM as LViewVM)!;

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            this.ListVM.IsFocus = true;
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            this.ListVM.IsFocus = false;
        }


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
