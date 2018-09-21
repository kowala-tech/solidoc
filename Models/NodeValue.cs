namespace Solidoc.Models
{
    public sealed class NodeValue
    {
        public TypeDescriptor ArgumentTypes { get; set; }
        public string HexValue { get; set; }
        public int? Id { get; set; }
        public bool? IsConstant { get; set; }
        public bool? IsLValue { get; set; }
        public bool? IsPure { get; set; }
        public string Kind { get; set; }
        public bool? LValueRequested { get; set; }
        public string NodeType { get; set; }
        public string Src { get; set; }
        public string Subdenomination { get; set; }
        public TypeDescriptor TypeDescriptions { get; set; }
        public string Value { get; set; }
    }
}