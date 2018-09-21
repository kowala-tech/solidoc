using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class Serializer
    {
        public Serializer(List<Contract> contracts, string outputDirectory)
        {
            this.Contracts = contracts;
            this.OutputDirectory = outputDirectory;
        }

        private List<Contract> Contracts { get; }
        private string OutputDirectory { get; }

        public void Serialize()
        {
            foreach (var contract in this.Contracts)
            {
                this.Serialize(contract);
            }
        }

        private void Serialize(Contract contract)
        {
            string template = TemplateHelper.Contract;

            var type = typeof(ISerializer);
            var generators = type.GetTypeMembersNotAbstract<ISerializer>();

            template = generators.Aggregate(template, (current, generator) => generator.Serialize(contract, current, this.Contracts));

            template = template.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
            template = Regex.Replace(template, @"(^\s*(\r|\n)){2,}|^\s+(\r|\n)?\Z", Environment.NewLine, RegexOptions.Multiline);


            string path = Path.Combine(this.OutputDirectory, $"{contract.ContractName}.md");
            Console.WriteLine(I18N.WritingPath, path);

            File.WriteAllText(path, template, new UTF8Encoding(true));
        }
    }
}