using System.Collections.Generic;
using System.Linq;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class EventSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var nodes = NodeHelper.GetEvents(contract).ToList();

            if (!nodes.Any())
            {
                return template.Replace("{{Events}}", string.Empty);
            }

            var builder = new EventBuilder(nodes);

            template = template.Replace("{{Events}}", builder.Build());
            return template;
        }
    }
}