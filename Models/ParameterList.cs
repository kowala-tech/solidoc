using System.Collections.Generic;

namespace Solidoc.Models
{
    public sealed class ParameterList
    {
        public int? Id { get; set; }
        public string NodeType { get; set; }
        public IEnumerable<Node> Parameters { get; set; }
        public string Src { get; set; }
    }
}