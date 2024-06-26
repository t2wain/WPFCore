﻿using System.ComponentModel;
using WPFCore.Common.ElectIndex;
using WPFCore.Shared.UI;
using WPFCore.Shared.UI.TV;

namespace WPFCore.ElectIndex.TV
{
    /// <summary>
    /// View model for each tree node.
    /// </summary>
    public class NodeVM : TreeNodeVM
    {
        public NodeVM() { }

        public TIndexNodeEnum NodeType { get; set; }

        /// <summary>
        /// TODO: allow each tree node to maintain filter info
        /// </summary>
        public NodeFilter? Filter { get; set; }

        /// <summary>
        /// TODO: allow each tree node to maintain business data
        /// </summary>
        public TNodeData? DataItem { get; set; }

        protected TreeVM ParentVM => (TreeVM)this.Parent;

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            switch (args.PropertyName)
            {
                case nameof(IsExpanded):
                    if (this.IsExpanded && this.IsChildrenNotPopulated)
                    {
                        this.PopulateData();
                    }
                    break;
            }
        }

        #region Populate data

        public override void PopulateData()
        {
            var t = this.PopulateDataAsync();
        }

        // asynchronous opertaion
        protected async Task PopulateDataAsync()
        {
            Utility.SetWaitCursor();
            this.Children.Clear();
            this.Children.Add(ParentVM.CreateLoadingNode());
            this.RaiseSelectedItemChildrenDataRefreshed();
            var lstNodes = await this.ParentVM.GetChildren(this);

            // back to UI thread to populate child nodes
            this.Children.Clear(); // clear loading node
            this.AddChildrenNodes(lstNodes);
            this.AddNoResultFound(); // determine if there are children returned
            this.IsExpanded = true;

            this.RaiseSelectedItemChildrenDataRefreshed();
            Utility.SetNormalCursor();
        }

        protected virtual void RaiseSelectedItemChildrenDataRefreshed()
        {
            if (this.Parent.SelectedItem is NodeVM sn && sn.ID == this.ID)
            {
                // raise event to indicate data is refreshed
                this.ParentVM.RaisePropertyChangeEvent(TreeVM.SelectedItemChildrenRefreshedEvent);;
            }
        }

        protected void AddNoResultFound()
        {
            if (this.Children.Count == 0)
                this.Children.Add(ParentVM.CreateNoResultNode());
        }

        #endregion
    }
}
