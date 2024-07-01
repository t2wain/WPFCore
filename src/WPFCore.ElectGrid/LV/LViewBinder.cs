using System.Windows;
using System.Windows.Controls;
using WPFCore.Common.UI;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public class LViewBinder : ListViewBinder
    {
        #region Init

        public override void InitListView(ListView lv, ListViewVM vm)
        {
            base.InitListView(lv, vm);
            SetupGridView();
            ConfigureEvent(lv);

        }

        public override void Dispose()
        {
            this.ListViewControl.GotFocus -= this.OnFocus;
            base.Dispose();
        }

        #endregion

        #region Configure events

        void ConfigureEvent(ListView lv)
        {
            lv.GotFocus += this.OnFocus;
        }

        void OnFocus(object sender, RoutedEventArgs e)
        {
            this.ListViewControl.RaiseEvent(new(WPFCoreApp.CustomControlFocusEvent, this));
        }

        #endregion

        #region Setup GridView

        void SetupGridView()
        {
            this.ListViewControl.ItemContainerStyle = GridUtility.CreateViewStyle();
        }

        #endregion
    }
}
