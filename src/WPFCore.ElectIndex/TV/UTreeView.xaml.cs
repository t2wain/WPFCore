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

        /// <summary>
        /// Setup view models and configure control bindings
        /// </summary>
        public void Init(UTreeLBoxVM vm)
        {
            // configure event handlers and command bindings
            this._tvwBind = new TreeIndexBinder();
            this._tvwBind.InitTreeView(this._tvwIndex, vm.TreeVM);

            // configure event handlers and command bindings
            this._lbxBind = new LBoxBinder();
            this._lbxBind.InitListView(this._lbxData, vm.LBoxVM);
        }

        public void Dispose()
        {
            this._tvwBind?.Dispose();
            this._lbxBind?.Dispose();
        }
    }
}
