using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class StructSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var builder = new StringBuilder();
            var nodes = NodeHelper.GetStructs(contract).ToList();

            if (!nodes.Any())
            {
                return template.Replace("{{Structs}}", builder.ToString());
            }

            builder.Append($"## {I18N.Structs}");
            builder.Append(Environment.NewLine);

            foreach (var node in nodes)
            {
                builder.Append(StructHelper.GenerateStruct(node));
            }

            template = template.Replace("{{Structs}}", builder.ToString());
            return template;
        }
    }
}