using System.Collections.Generic;
using System.Text;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class ArgumentBuilder
    {
        public ArgumentBuilder(string documentation, IEnumerable<Node> parameters)
        {
            this.Documentation = documentation;
            this.Parameters = parameters;
        }

        public string Documentation { get; }
        public IEnumerable<Node> Parameters { get; }

        public string Build()
        {
            var builder = new StringBuilder();

            foreach (var parameter in this.Parameters)
            {
                builder.Append("| ");
                builder.Append(parameter.Name);
                builder.Append(" | ");
                builder.Append(parameter.TypeDescriptions.TypeString.Replace("contract ", ""));
                builder.Append(" | ");
                string doc = DocumentationHelper.Get(this.Documentation, "param " + parameter.Name);
                builder.Append(doc);
                builder.Append(" | ");
            }

            return builder.ToString();
        }
    }
}