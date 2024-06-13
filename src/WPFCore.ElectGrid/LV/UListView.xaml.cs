using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFCore.Common.Data;
using WPFCore.Common.ElectIndex;
using WPFCore.Common.UI;

namespace WPFCore.ElectGrid.LV
{
    /// <summary>
    /// Interaction logic for UListView.xaml
    /// </summary>
    public partial class UListView : UserControl
    {
        public UListView()
        {
            InitializeComponent();
            this.Unloaded += this.OnUnloaded;
        }

        private LViewBinder _lvwBind = null!;

        public LViewVM VM { get; set; } = null!;

        public void Init(LViewVM vm)
        {
            this.VM = vm;
            this._lvwBind = new LViewBinder();
            this._lvwBind.InitListView(_lvwData, vm);
            this.ConfigureCommands();
            this.ConfigureEvent();
        }

        #region Configure commands

        private void ConfigureCommands()
        {
            this.CommandBindings.Add(new(TACommands.ViewDetail, this.OnViewDetail, this.OnViewDetailCanExecuted));
        }

        virtual protected void OnViewDetail(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                this.VM.ShowReport(eq.ID)
                    .ContinueWith(t => Task.CompletedTask);
            }
        }

        virtual protected void OnViewDetailCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter is TNodeData n
                && n.Data is EquipItem eq
                && !String.IsNullOrWhiteSpace(eq.ID))
            {
                e.CanExecute = true;
            }
            else { e.CanExecute = false; }
        }

        #endregion

        #region Configure events

        void ConfigureEvent()
        {
            this.GotFocus += this.OnFocus;
        }

        void OnFocus(object sender, RoutedEventArgs e)
        {
            base.RaiseEvent(new(WPFCoreApp.CustomControlFocusEvent, this));
        }

        #endregion

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this._lvwBind?.Dispose();
        }
    }
}
