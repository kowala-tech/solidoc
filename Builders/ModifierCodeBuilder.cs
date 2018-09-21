using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solidoc.Models;

namespace Solidoc.Builders
{
    public sealed class ModifierCodeBuilder
    {
        public ModifierCodeBuilder(Node node)
        {
            this.Node = node;
        }

        public Node Node { get; }

        public string Build()
        {
            var builder = new StringBuilder();

            var parameters = this.Node.Parameters.Parameters ?? new List<Node>();

            var parameterList = from parameter in parameters
                let argumentName = parameter.Name
                let dataType = parameter.TypeDescriptions.TypeString.Replace("contract ", "")
                select dataType + " " + argumentName;


            builder.Append("```js");
            builder.Append(Environment.NewLine);
            builder.Append($"modifier {this.Node.Name}(");


            builder.Append(string.Join(", ", parameterList));

            builder.Append(") ");

            builder.Append(this.Node.Visibility.ToString().ToLower());
            builder.Append(Environment.NewLine);
            builder.Append("```");

            return builder.ToString();
        }
    }
}