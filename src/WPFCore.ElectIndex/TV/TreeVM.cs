﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPFCore.Common.ElectIndex;
using WPFCore.Shared.UI.TV;
using CM = WPFCore.Shared.UI.TV.TNCommands;
using CM2 = WPFCore.Common.UI.TACommands;
using NT = WPFCore.Common.ElectIndex.TIndexNodeEnum;

namespace WPFCore.ElectIndex.TV
{
    // view model for a TreeView control
    public class TreeVM : TreeViewVM
    {
        // These are faux properties. The intention is to use
        // the PropertyChanged notification to communicate an event.
        public const string SelectedItemChildrenRefreshedEvent = "WPFCore.ElectIndex.TV.TreeVM.SelectedItemChildrenDataRefreshedEvent";
        public const string ExecuteViewDetailCmdEvent = "WPFCore.ElectIndex.TV.TreeVM.ExecuteViewDetailCmdEvent";

        private readonly TVRepo _repo;

        public TreeVM(TVRepo repo) : base()
        {
            this._repo = repo;
            this.InitData();
        }

        #region Command bindings

        public override bool IsContextMenuAllow(INotifyPropertyChanged di)
        {
            var allow = base.IsContextMenuAllow(di);

            var n = di as NodeVM;
            if (n == null)
                return false;

            switch (n.NodeType)
            {
                case NT.Dummy:
                    allow = false;
                    break;
            }

            return allow;
        }

        protected override List<RoutedUICommand> GetContextCommands()
        {
            var lst = base.GetContextCommands();
            lst.Add(CM2.SetFilter);
            lst.Add(CM2.ClearFilter);
            lst.Add(CM2.ViewDetail);

            return lst;
        }

        public override bool IsCommandCanExecute(string cmdName, INotifyPropertyChanged di)
        {
            bool allow = false;

            var n = di as NodeVM;
            if (n == null)
                return allow;

            var t = n.NodeType;
            switch (cmdName)
            {
                case CM.ExpandAllMsg:
                    allow = t switch 
                    { 
                        NT.Loads => true, 
                        _ => false 
                    };
                    break;
                case CM.RefreshMsg:
                case CM.CollapseAllMsg:
                case CM.ExpandMsg:
                case CM.CollapseMsg:
                    allow = !n.IsLeafNode;
                    break;
                case CM2.SetFilterMsg:
                case CM2.ClearFilterMsg:
                    allow = t switch 
                    { 
                        NT.NoResult | NT.Dummy => true, 
                        _ => false 
                    };
                    break;
                case CM2.ViewDetailMsg:
                    allow = t switch
                    {
                        NT.NoResult | NT.Dummy => false,
                        _ => n.IsLeafNode
                    };
                    break;
            }

            return allow;
        }

        #endregion

        #region Retrieve nodes

        // populate root nodes
        protected void InitData()
        {
            this.Root =  new ObservableCollection<INotifyPropertyChanged>(this._repo.GetIndexRoot(this).Result);
        }

        internal INotifyPropertyChanged CreateLoadingNode() =>
            _repo.CreateLoadingNode();

        internal INotifyPropertyChanged CreateNoResultNode() =>
            _repo.CreateNoResultNode();

        internal Task<List<INotifyPropertyChanged>> GetChildren(INotifyPropertyChanged node)
        {
            if (node is NodeVM n)
                return _repo.GetChildren(n.NodeType, n.DataItem, this);
            else return Task.FromResult(new List<INotifyPropertyChanged>() { });
        }

        #endregion

        #region Raise events

        public virtual void SendMessage(TNodeData data)
        {

        }

        #endregion
    }
}
