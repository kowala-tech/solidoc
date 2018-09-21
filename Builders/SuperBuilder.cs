using System.Collections.Generic;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class SuperBuilder
    {
        public SuperBuilder(Node node, IEnumerable<Contract> contracts)
        {
            this.Node = node;
            this.Contracts = contracts;
        }

        public Node Node { get; }
        public IEnumerable<Contract> Contracts { get; }

        public string Build()
        {
            int super = this.Node.SuperFunction ?? 0;
            var result = new Node();
            var baseContract = new Contract();

            if (super == 0)
            {
                return string.Empty;
            }

            foreach (var contract in this.Contracts)
            {
                result = contract.Ast.Nodes.FindNodeById(super);

                if (result == null)
                {
                    continue;
                }

                baseContract = contract;
                break;
            }

            return result == null
                ? string.Empty
                : ":small_red_triangle: " + string.Format(I18N.OverridesSuperFunction, $"[{baseContract.ContractName}.{this.Node.Name}](#{baseContract.ContractName}#{this.Node.Name.ToLower()})");
        }
    }
}