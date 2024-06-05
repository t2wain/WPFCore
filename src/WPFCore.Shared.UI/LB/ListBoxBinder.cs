﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.LB
{
    /// <summary>
    /// Setup event handlers and command bindings for the ListBox control
    /// and retrieve the view model from the DataContext.
    /// </summary>
    public class ListBoxBinder : IDisposable
    {
        private ListBox? _lvw;
        public ListBox? ListBoxControl
        {
            get { return this._lvw; }
            set
            {
                this._lvw = value;
                if (this._lvw != null && !DesignerProperties.GetIsInDesignMode(this._lvw))
                    this.InitListView(_lvw);
            }
        }

        protected ListBoxVM? VM { get; set; }

        RoutedEventHandler _h1 = null!;

        virtual protected void InitListView(ListBox lv)
        {
            _h1 = new RoutedEventHandler(this.OnDoubleClick);
            lv.AddHandler(Control.MouseDoubleClickEvent, _h1);
            this.ConfigCommands(lv);

            if (lv.DataContext is ListBoxVM vm)
            {
                this.VM = vm;
                this.VM.PropertyChanged += this.ListenPropertyChangedOnVM;
            }
        }

        public ListBoxItemVM? SelectedItem =>
            this._lvw?.SelectedItem as ListBoxItemVM;

        virtual protected void OnDoubleClick(object sender, RoutedEventArgs e) { }

        #region Config Commands

        // command handler Collapse All
        virtual protected void OnSelectAll(object sender, RoutedEventArgs e)
        {
            this.VM?.SelectAllItems();
        }

        virtual protected void OnSelectAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.SelectAllMsg);
        }

        // command handler Collapse All
        virtual protected void OnUnSelectAll(object sender, RoutedEventArgs e)
        {
            this.VM?.UnSelectAllItems();
        }

        virtual protected void OnUnSelectAllCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.IsCommandCanExecute(TNCommands.UnselectAllMsg);
        }

        // Configure the handler for the commands
        virtual protected void ConfigCommands(ListBox lv)
        {
            foreach (var cb in this.GetCommandBindings())
                lv.CommandBindings.Add(cb);
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.SelectAll, this.OnSelectAll, this.OnSelectAllCanExecuted),
                new CommandBinding(TNCommands.UnselectAll, this.OnUnSelectAll, this.OnUnSelectAllCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allow = false;
            switch (cmdName)
            {
                case TNCommands.SelectAllMsg:
                case TNCommands.UnselectAllMsg:
                    if (this.VM != null)
                        allow = this.VM.ListItems.Count > 0;
                    break;
            }
            return allow;
        }

        #endregion

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }

        public void Dispose()
        {
            if (this._lvw != null)
            {
                this._lvw.RemoveHandler(Control.MouseDoubleClickEvent, _h1);
                this._lvw.CommandBindings.Clear();
                if (this.VM != null)
                    this.VM.PropertyChanged -= this.ListenPropertyChangedOnVM;
                this._lvw = null;
            }
        }
    }
}
