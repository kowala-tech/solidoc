using System.Collections.Generic;

namespace Solidoc.Models
{
    public class Abi
    {
        public bool Constant { get; set; }
        public IEnumerable<AbiInput> Inputs { get; set; }
        public string Name { get; set; }
        public IEnumerable<AbiOutput> Outputs { get; set; }
        public bool Payable { get; set; }
        public string StateMutability { get; set; }
        public string Type { get; set; }
        public bool? Anonymous { get; set; }
    }
}