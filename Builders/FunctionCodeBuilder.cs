using System;
using System.Linq;
using System.Text;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class FunctionCodeBuilder
    {
        public FunctionCodeBuilder(Node node)
        {
            this.Node = node;
        }

        private Node Node { get; }

        public string Build()
        {
            var builder = new StringBuilder();

            var parameters = this.Node.Parameters.Parameters.ToList();
            string documentation = this.Node.Documentation;

            string returnDocumentation = DocumentationHelper.Get(documentation, "return");

            var parameterList = from parameter in parameters
                let argumentName = parameter.Name
                let dataType = parameter.TypeDescriptions.TypeString.Replace("contract ", "")
                select dataType + " " + argumentName;

            var modifierList = (from modifier in this.Node.Modifiers
                select modifier.ModifierName.Name).ToList();


            builder.Append("```js");
            builder.Append(Environment.NewLine);
            builder.Append($"function {this.Node.Name}(");


            builder.Append(string.Join(", ", parameterList));

            builder.Append(") ");

            builder.Append(this.Node.Visibility.ToString().ToLower());

            if (this.Node.Payable.HasValue && this.Node.Payable.Value)
            {
                builder.Append(" payable");
            }

            if (!new[] {"nonpayable", ""}.Contains(this.Node.StateMutability))
            {
                builder.Append($" {this.Node.StateMutability}");
            }

            if (modifierList.Any())
            {
                builder.Append($" {string.Join(" ", modifierList)}");
            }


            builder.Append(Environment.NewLine);
            builder.Append(this.GetReturnParameters());
            builder.Append(Environment.NewLine);
            builder.Append("```");

            if (string.IsNullOrWhiteSpace(returnDocumentation))
            {
                return builder.ToString();
            }

            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append($"**{I18N.Returns}**");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append(returnDocumentation);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        private string GetReturnParameters()
        {
            var builder = new StringBuilder();

            var returnParameters = NodeHelper.GetReturnParameters(this.Node).ToList();

            if (!returnParameters.Any())
            {
                return builder.ToString();
            }

            builder.Append("returns(");

            var returnList = returnParameters.Select(parameter => $"{parameter.Name} {parameter.TypeDescriptions.TypeString}".Trim()).ToList();

            builder.Append(string.Join(", ", returnList));

            builder.Append(")");


            return builder.ToString();
        }
    }
}