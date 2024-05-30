using CommunityToolkit.Mvvm.ComponentModel;
using System.Data;
using WPFCore.Shared.UI;
using WPFCore.Shared.UI.LV;

namespace WPFCore.ElectGrid.LV
{
    public partial class LViewVM : ListViewVM
    {

        IReportDS _ds = null!;

        public LViewVM(IReportDS ds)
        {
            this._ds = ds;
            this.Init();
        }

        [ObservableProperty]
        LViewEnum _viewType;

        public override void PopulateData()
        {
            this.QueryDataAsync();
        }

        protected async void QueryDataAsync()
        {
            Utility.SetWaitCursor();
            DataView? dv = null;
            switch (this.ViewType)
            {
                case LViewEnum.Motors:
                    dv = await this._ds.GetMotors();
                    break;
                case LViewEnum.OtherElectricalEquipment:
                    dv = await this._ds.GetOtherElectricalEquipment();
                    break;
                case LViewEnum.Transformers:
                    dv = await this._ds.GetTransformers();
                    break;
            }

            if (dv != null)
                this.ListData = dv;
            Utility.SetNormalCursor();
        }
    }
}
