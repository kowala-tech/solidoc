using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class EnumSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var builder = new StringBuilder();
            var nodes = NodeHelper.GetEnumerators(contract).ToList();

            if (!nodes.Any())
            {
                return template.Replace("{{Enumerators}}", builder.ToString());
            }

            builder.Append($"## {I18N.Enums}");
            builder.Append(Environment.NewLine);

            foreach (var node in nodes)
            {
                var enumbuilder = new EnumeratorBuilder(node);
                builder.Append(enumbuilder.Build());
            }

            template = template.Replace("{{Enumerators}}", builder.ToString());
            return template;
        }
    }
}