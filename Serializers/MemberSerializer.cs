using System.Collections.Generic;
using System.Linq;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class MemberSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var nodes = NodeHelper.GetMembers(contract).ToList();

            if (!nodes.Any())
            {
                return template.Replace("{{Members}}", string.Empty);
            }

            var builder = new MemberBuilder(nodes);

            template = template.Replace("{{Members}}", builder.Build());
            return template;
        }
    }
}