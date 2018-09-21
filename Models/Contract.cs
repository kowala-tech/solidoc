using System.Collections.Generic;

namespace Solidoc.Models
{
    public sealed class Contract
    {
        public string ContractName { get; set; }
        public IEnumerable<Abi> Abi { get; set; }
        public string Bytecode { get; set; }
        public string DeployedBytecode { get; set; }
        public string SourceMap { get; set; }
        public string DeployedSourceMap { get; set; }
        public string Source { get; set; }
        public string SourcePath { get; set; }
        public Ast Ast { get; set; }
        public Ast LegacyAst { get; set; }
        public Compiler Compiler { get; set; }
    }
}