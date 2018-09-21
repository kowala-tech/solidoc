using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Models;

namespace Solidoc.Builders
{
    public sealed class EnumeratorBuilder
    {
        public EnumeratorBuilder(Node node)
        {
            this.Node = node;
        }

        public Node Node { get; }

        public string Build()
        {
            var builder = new StringBuilder();

            builder.Append($"### {this.Node.Name}");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("```js");
            builder.Append(Environment.NewLine);
            builder.Append($"enum {this.Node.Name} {{");
            builder.Append(Environment.NewLine);

            var members = (from member in this.Node.Members ?? new List<Node>()
                    select $" {member.Name}")
                .ToList();

            builder.Append(string.Join("," + Environment.NewLine, members));

            builder.Append(Environment.NewLine);

            builder.Append("}");
            builder.Append(Environment.NewLine);
            builder.Append("```");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }
    }
}