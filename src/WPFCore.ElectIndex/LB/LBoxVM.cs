﻿using System.ComponentModel;
using System.Windows.Input;
using WPFCore.ElectIndex.TV;
using WPFCore.Shared.UI.LB;
using NT = WPFCore.ElectIndex.TV.TIndexNodeEnum;

namespace WPFCore.ElectIndex.LB
{
    public class LBoxVM : ListBoxVM
    {
        public LBoxVM()
        {
            this.Init();
        }

        INotifyPropertyChanged? _nodeData;
        public INotifyPropertyChanged? CurrentIndexNode
        {
            get { return this._nodeData; }
            set { SetProperty(ref this._nodeData, value); }
        }

        public override void PopulateData()
        {
            this.ListItems.Clear();
            if (this.CurrentIndexNode is NodeVM n && n.Children.Count > 0)
            {
                switch (n.NodeType)
                {
                    case NT.Motors:
                    case NT.OtherElectricalEquipment:
                    case NT.Generators:
                    case NT.Transformers:
                    case NT.VariableFrequencyDrives:
                    case NT.PowerDistributionBoards:
                        var c1 = n.Children.First() as NodeVM;
                        if (c1 == null
                            || c1.NodeType == NT.NoResult
                            || c1.NodeType == NT.Loading)
                            return;
                        var q = from n3 in n.Children.Cast<NodeVM>()
                                select new LBoxItemVM
                                {
                                    Name = n3.Name,
                                    Data = n3.DataItem,
                                    Parent = this
                                };
                        this.AddItems(q.ToList());
                        break;
                }
            }
        }

        protected override List<RoutedUICommand> GetContextCommands()
        {
            var lst = base.GetContextCommands();
            lst.Add(TACommands.ViewDetail);
            return lst;
        }

        public void SendMessage(TNodeData data) { }
    }
}
