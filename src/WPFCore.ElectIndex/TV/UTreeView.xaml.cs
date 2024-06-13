using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using WPFCore.ElectIndex.LB;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// This control implements a combination of a TreeView 
    /// and a ListBox. The ListBox will display the 
    /// children of the selected tree node.
    /// </summary>
    public partial class UTreeView : UserControl
    {
        #region Init

        public UTreeView()
        {
            InitializeComponent();
            this.Unloaded += this.OnUnloaded;
        }

        private TreeIndexBinder _tvwBind = null!;
        private LBoxBinder _lbxBind = null!;

        public UTreeLBoxVM VM { get; protected set; } = null!;

        /// <summary>
        /// Setup view models and configure control bindings
        /// </summary>
        public void Init(UTreeLBoxVM vm)
        {
            this.DataContext = vm;
            this.VM = vm;

            // configure event handlers and command bindings
            this._tvwBind = new TreeIndexBinder();
            this._tvwBind.InitTreeView(this._tvwIndex, vm.TreeVM);

            // configure event handlers and command bindings
            this._lbxBind = new LBoxBinder();
            this._lbxBind.InitListView(this._lbxData, vm.LBoxVM);

            ConfigureEvents();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            VM.PropertyChanged -= OnElectIndexPropertyChanged;
            this.GotFocus -= this.OnFocus;
            this._tvwBind?.Dispose();
            this._lbxBind?.Dispose();
            VM = null!;
        }

        #endregion

        #region Configure events

        protected void ConfigureEvents()
        {

            VM.PropertyChanged += OnElectIndexPropertyChanged;
            this.GotFocus += this.OnFocus;
        }

        private void OnElectIndexPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(VM.ItemCount):
                    base.RaiseEvent(new(WPFCoreApp.ItemCountChangedEvent, this));
                    break;
                case UTreeLBoxVM.ExecuteViewDetailCmdTVEvent:
                    if (VM.TreeVM.SelectedItem is NodeVM t && t.DataItem != null)
                        base.RaiseEvent(new ViewItemDetailEventArgs(WPFCoreApp.ViewItemDetailEvent, this, t.DataItem));
                    break;
                case UTreeLBoxVM.ExecuteViewDetailCmdLBEvent:
                    if (VM.LBoxVM.SelectedItems.FirstOrDefault() is LBoxItemVM l && l.Data != null)
                        base.RaiseEvent(new ViewItemDetailEventArgs(WPFCoreApp.ViewItemDetailEvent, this, l.Data));
                    break;
            }
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            base.RaiseEvent(new(WPFCoreApp.CustomControlFocusEvent, this));
        }

        #endregion
    }
}
