using WPFCore.Data;

namespace WPFCore.ElectIndex.TV
{
    // TODO: allow each tree node to maintain business data
    public class TNodeData
    {
        public TIndexNodeEnum NodeType { get; set; }
        public EquipItem? Data { get; set; }
    }
}
