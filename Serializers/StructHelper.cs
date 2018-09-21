using System;
using System.Linq;
using System.Text;
using Solidoc.Models;

namespace Solidoc.Serializers
{
    public static class StructHelper
    {
        public static string GenerateStruct(Node node)
        {
            var builder = new StringBuilder();

            builder.Append($"### {node.Name}");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("```js");
            builder.Append(Environment.NewLine);
            builder.Append($"struct {node.Name} {{");
            builder.Append(Environment.NewLine);

            var members = (from member in node.Members
                    select $"  {member.TypeDescriptions.TypeString} {member.Name}")
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