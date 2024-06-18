using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;
using WPFCore.Data.Report;
using WPFCore.ElectGrid.LV;
using WPFCore.Shared.UI;

namespace WPFCore.ElectGrid.TC
{
    public partial class TabItemVM : ObservableObject
    {

        [ObservableProperty]
        string? _name = "Test";

        [ObservableProperty]
        bool _isSelected;

        [ObservableProperty]
        INotifyPropertyChanged? _tabDataContext;

        public TabItemEnum ItemType { get; set; }

        public string? ItemID { get; set; }

        #region Report

        public ReportResult? Report { get; set; }


        #endregion

    }
}
