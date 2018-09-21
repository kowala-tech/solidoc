using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class ContractSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            string contractName = contract.ContractName;

            var contractNode = NodeHelper.GetContractNode(contract);

            string documentation = contractNode.Documentation;
            string contractTitle = DocumentationHelper.Get(documentation, "title");
            string notice = DocumentationHelper.GetNotice(documentation);
            var anchors = contracts.Select(item => $"- [{item.ContractName}]({item.ContractName}.md)").ToList();


            var dependencies = NodeHelper.GetBaseContracts(contract) ?? new List<Node>();

            var dependencyList = dependencies.Select(dependency => $"[{dependency.BaseName.Name}]({dependency.BaseName.Name}.md)").ToList();


            string title = $"{contract.ContractName}.sol";

            if (!string.IsNullOrWhiteSpace(contractTitle))
            {
                title = $"{Regex.Replace(contractTitle, @"\r\n?|\n", " ")} ({contract.ContractName}.sol)";
            }


            string contractInheritencePath = string.Empty;

            if (dependencyList.Any())
            {
                contractInheritencePath = $"**contract {contractName} is {string.Join(", ", dependencyList)}**";
            }


            template = template.Replace("{{ContractName}}", contractName);
            template = template.Replace("{{ContractTitle}}", title);
            template = template.Replace("{{ContractDescription}}", notice);
            template = template.Replace("{{ContractInheritencePath}}", contractInheritencePath);


            template = template.Replace("{{AllContractsAnchor}}", string.Join(Environment.NewLine, anchors));
            template = template.Replace("{{ABI}}", JsonConvert.SerializeObject(contract.Abi, Formatting.Indented));


            var builder = new ConstructorBuilder(contract);
            return builder.Build(template);
        }
    }
}