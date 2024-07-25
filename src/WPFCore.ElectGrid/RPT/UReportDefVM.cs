using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WPFCore.Common.Data;
using WPFCore.Data.Report;
using WPFCore.Shared.UI.DLG;

namespace WPFCore.ElectGrid.RPT
{
    public partial class UReportDefVM : DialogVM
    {
        private readonly IReportDS? _ds = null!;

        public UReportDefVM(IReportDS? ds = null) : base() 
        {
            this.RefeshCmd = new RelayCommand(this.OnRefresh, this.OnAllow2);
            this.SaveCmd = new RelayCommand(this.OnSave, this.OnAllow2);
            this.ExportCmd = new RelayCommand(this.OnExport, this.OnAllow);
            this.ImportCmd = new RelayCommand(this.OnImport, this.OnAllow);
            this._ds = ds;
        }

        [ObservableProperty]
        ReportDefinition _reportDef = null!;

        [ObservableProperty]
        RelayCommand _refeshCmd = null!;

        [ObservableProperty]
        RelayCommand _saveCmd = null!;

        [ObservableProperty]
        RelayCommand _exportCmd = null!;

        [ObservableProperty]
        RelayCommand _importCmd = null!;

        bool _busy = false;

        protected bool OnAllow() => !this._busy;

        protected bool OnAllow2() => !this._busy && this._ds != null;

        protected async void OnRefresh()
        {
            try
            {
                this._busy = true;
                this.RefeshCmd.NotifyCanExecuteChanged();
                var cols = await this._ds!.GetUpdatedColumnDefinitions(this.ReportDef);
                this.ReportDef.Columns = cols;
            }
            catch
            {
                this.ReportDef.Columns = new();
            }
            finally
            {
                this._busy = false;
                this.RefeshCmd.NotifyCanExecuteChanged();
                this.OnPropertyChanged(nameof(this.ReportDef));
            }
        }

        protected async void OnSave()
        {
            try
            {
                this._busy = true;
                this.RefeshCmd.NotifyCanExecuteChanged();
                await this._ds!.SaveReportDefinition(this.ReportDef);
            }
            finally
            {
                this._busy = false;
                this.RefeshCmd.NotifyCanExecuteChanged();
            }
        }

        protected void OnExport()
        {

        }

        protected void OnImport()
        {

        }

    }
}
