using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Models;

namespace Solidoc.Builders
{
    public sealed class EventBuilder
    {
        public EventBuilder(IEnumerable<Node> nodes)
        {
            this.Nodes = nodes;
        }

        public IEnumerable<Node> Nodes { get; }

        public string Build()
        {
            var builder = new StringBuilder();

            builder.Append($"**{I18N.Events}**");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("```js");
            builder.Append(Environment.NewLine);

            foreach (var node in this.Nodes)
            {
                var parameterList = from parameter in node.Parameters.Parameters.ToList()
                    let argumentName = parameter.Name
                    let dataType = parameter.TypeDescriptions.TypeString.Replace("contract ", "")
                    let indexed = parameter.Indexed ?? false
                    select dataType + (indexed ? " indexed " : " ") + argumentName;

                builder.Append($"event {node.Name}({string.Join(", ", parameterList)});");

                builder.Append(Environment.NewLine);
            }

            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("```");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}