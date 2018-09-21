using System.Collections.Generic;

namespace Solidoc.Models
{
    public sealed class Modifier
    {
        public int? Id { get; set; }
        public Node ModifierName { get; set; }
        public string NodeType { get; set; }
        public string Src { get; set; }
        public IEnumerable<Node> Arguments { get; set; }
    }
}