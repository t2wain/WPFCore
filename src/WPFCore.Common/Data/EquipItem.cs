namespace WPFCore.Common.Data
{
    public record EquipItem
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public int EquipClass { get; set; }
        public int EquipSubClass { get; set; }
    }
}
