using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.UI;
using WPFCore.Shared.UI.TV;

namespace WPFCore.Shared.UI.DG
{
    public class DataGridBinder : IDisposable
    {
        #region Other

        public DataGrid GridControl { get; protected set; } = null!;

        public DataGridVM VM { get; protected set; } = null!;

        virtual public void InitDataGrid(DataGrid dg, DataGridVM vm)
        {
            this.GridControl = dg;

            this.ConfigureEvent(dg, vm);

            this.ConfigCommands();

            this.VM = vm;
            dg.DataContext = vm;
        }

        public virtual void Dispose()
        {
            var dg = this.GridControl;
            dg.RemoveHandler(DataGrid.ContextMenuOpeningEvent, _h1);
            dg.CopyingRowClipboardContent -= this.OnCopying;

            var vm = this.VM;
            vm.PropertyChanged -= this.ListenPropertyChangedOnVM;

            this.GridControl = null!;
        }

        // changes from the view model
        virtual protected void ListenPropertyChangedOnVM(object? sender, PropertyChangedEventArgs e) { }

        #endregion

        #region DataGrid control event handler

        RoutedEventHandler _h1 = null!;

        void ConfigureEvent(DataGrid dg, DataGridVM vm)
        {
            // Configure handlers for DataGrid events

            _h1 = new RoutedEventHandler(this.OnContextMenuOpen);
            dg.AddHandler(DataGrid.ContextMenuOpeningEvent, _h1);

            dg.CopyingRowClipboardContent += this.OnCopying;

            vm.PropertyChanged += this.ListenPropertyChangedOnVM;
        }

        virtual protected void OnCopying(object? sender, DataGridRowClipboardEventArgs e)
        {
            var q = DataGridUtility.ParseCellContent(e.ClipboardRowContent);
            if (q.Invalid)
            {
                e.ClipboardRowContent.Clear();
                e.ClipboardRowContent.AddRange(q.Contents);
            }
        }

        virtual protected void OnContextMenuOpen(object sender, RoutedEventArgs e)
        {
            e.Handled = !this.VM.IsContextMenuAllow(null);
        }

        #endregion

        #region Config Commands

        // Configure the handler for the commands
        void ConfigCommands()
        {
            foreach (var cb in this.GetCommandBindings())
                this.GridControl.CommandBindings.Add(cb);
        }

        // command handler for Refresh
        virtual protected void OnRefresh(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.VM.RefreshData();
        }

        virtual protected void OnSelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.GridControl.SelectAll();
        }

        virtual protected void OnUnSelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            this.GridControl.UnselectAll();
        }

        private void OnPaste(object sender, ExecutedRoutedEventArgs e)
        {
            var val = Clipboard.GetText();
            DataGridUtility.PasteIntoRowsAndColumns(val, this.GridControl);
        }

        virtual protected void OnCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command is RoutedUICommand cmd)
            {
                if (cmd.OwnerType == typeof(TNCommands))
                    e.CanExecute = this.IsCommandCanExecute($"TNCommands.{cmd.Name}Msg");
                else e.CanExecute = true;
            }
        }

        // Configure the handler for the commands
        virtual protected IEnumerable<CommandBinding> GetCommandBindings()
        {
            var lst = new List<CommandBinding>
            {
                new CommandBinding(TNCommands.Refresh, this.OnRefresh, this.OnCommandCanExecuted),
                new CommandBinding(TNCommands.SelectAll, this.OnSelectAll, this.OnCommandCanExecuted),
                new CommandBinding(TNCommands.UnselectAll, this.OnUnSelectAll, this.OnCommandCanExecuted),
                new CommandBinding(ApplicationCommands.Paste, this.OnPaste, this.OnCommandCanExecuted)
            };

            return lst;
        }

        virtual protected bool IsCommandCanExecute(string cmdName)
        {
            var allowed = this.VM.IsCommandCanExecute(cmdName, null);
            return allowed;
        }

        #endregion
    }
}
