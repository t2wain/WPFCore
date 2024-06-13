﻿using System.ComponentModel;
using System.Windows;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;
using WPFCore.ElectGrid.LV;
using WPFCore.ElectIndex;
using WPFCore.ElectIndex.TV;
using WPFCore.Menu;
using WPFCore.Shared.UI.SB;
using NT = WPFCore.Common.ElectIndex.TIndexNodeEnum;

namespace WPFCore.App
{
    public class AppMainBinder : IDisposable
    {
        #region Init

        UMenu _menu = null!;
        UStatusBar _sb = null!;
        StatusBarVM _sbvm = null!;
        Window _mainWindow = null!;

        public void Init(
            Window mainWindow, 
            UMenu mainMenu, 
            UStatusBar statBar)
        {
            _menu = mainMenu;
            _sb = statBar;
            _mainWindow = mainWindow;

            _sbvm = _sb.Init();
            _menu.Init();

            ConfigureEvents(_mainWindow);
        }

        public void Dispose()
        {
            _lvm.PropertyChanged -= OnElectGridPropertyChanged;
            _mainWindow.RemoveHandler(WPFCoreApp.ItemCountChangedEvent, _h1);
            _mainWindow.RemoveHandler(WPFCoreApp.CustomControlFocusEvent, _h2);
            _mainWindow.RemoveHandler(WPFCoreApp.ViewItemDetailEvent, _h3);

            _menu = null!;
            _sb = null!;
            _sbvm = null!;
            _mainWindow = null!;
        }

        #endregion

        #region Event handler

        RoutedEventHandler _h1 = null!;
        RoutedEventHandler _h2 = null!;
        ViewItemDetailEventHandler _h3 = null!;

        void ConfigureEvents(Window w)
        {
            _h1 = new RoutedEventHandler(this.OnCustomFocus);
            w.AddHandler(WPFCoreApp.ItemCountChangedEvent, _h1);

            _h2 = new RoutedEventHandler(this.OnCustomFocus);
            w.AddHandler(WPFCoreApp.CustomControlFocusEvent, _h2);

            _h3 = new ViewItemDetailEventHandler(this.OnViewItemDetail);
            w.AddHandler(WPFCoreApp.ViewItemDetailEvent, _h3);
        }

        private void OnCustomFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is UTreeView ctl)
            {
                UpdateCountMsg(ctl.VM.ItemCount);
            }
        }

        private void OnViewItemDetail(object sender, ViewItemDetailEventArgs e)
        {
            ShowDetail(e.Data);
        }

        #endregion

        #region ElectIndex

        public void InitElectIndex(UTreeView tvw, UTreeLBoxVM tvm)
        {
            tvw.Init(tvm);
        }

        #endregion

        #region ElectGrid

        LViewVM _lvm = null!;
        public void InitElectGrid(UListView lvw, LViewVM lvm)
        {
                _lvm = lvm;
                lvm.PropertyChanged += OnElectGridPropertyChanged;
                lvw.Init(lvm);
        }

        private void OnElectGridPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_lvm.IsFocus):
                    if (_lvm.IsFocus)
                        UpdateCountMsg(this._lvm.ListData?.Count ?? 0);
                    break;
            }
        }

        #endregion

        #region Action

        private void ShowDetail(TNodeData? item)
        {
            switch (item?.NodeType)
            {
                case NT.Report:
                    _lvm.ShowReport(item?.Data?.ID)
                        .ContinueWith(t => Task.CompletedTask);
                    break;
            }
        }
        private void UpdateCountMsg(int count)
        {
            _sbvm.LeftMessage = count > 0 ? String.Format("Items: {0:#,###}", count) : "";
        }

        #endregion
    }
}