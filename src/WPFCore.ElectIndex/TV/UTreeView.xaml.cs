using System.Windows.Controls;
using WPFCore.ElectIndex.LB;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// This control implements a combination of a TreeView 
    /// and a ListBox. The ListBox will display the 
    /// children of the selected tree node.
    /// </summary>
    public partial class UTreeView : UserControl, IDisposable
    {
        public UTreeView()
        {
            InitializeComponent();
        }

        private TreeIndexBinder _tvwBind = null!;
        private LBoxBinder _lbxBind = null!;

        public void Init(UTreeLBoxVM vm)
        {
            this.DataContext = vm;
            this._tvwIndex.DataContext = vm.TreeVM;
            this._lbxData.DataContext = vm.LBoxVM;

            // configure event handlers and command bindings
            this._tvwBind = new TreeIndexBinder();
            this._tvwBind.TreeViewControl = this._tvwIndex;

            // configure event handlers and command bindings
            this._lbxBind = new LBoxBinder();
            this._lbxBind.ListBoxControl = this._lbxData;
        }

        public void Dispose()
        {
            this._tvwBind?.Dispose();
            this._lbxBind?.Dispose();
        }
    }
}
