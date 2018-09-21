using System.Collections.Generic;

namespace Solidoc.Models
{
    public sealed class Ast
    {
        public string AbsolutePath { get; set; }
        public dynamic ExportedSymbols { get; set; }
        public int? Id { get; set; }
        public string NodeType { get; set; }
        public IEnumerable<Node> Nodes { get; set; }
        public string Src { get; set; }
    }
}