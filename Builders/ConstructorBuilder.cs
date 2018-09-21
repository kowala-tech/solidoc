using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Builders
{
    public sealed class ConstructorBuilder
    {
        public ConstructorBuilder(Contract contract)
        {
            this.Contract = contract;
        }

        public Contract Contract { get; }

        public string Build(string template)
        {
            var node = NodeHelper.GetConstructorNode(this.Contract);
            string documentation = node.Documentation;
            string description = DocumentationHelper.GetNotice(documentation);

            var parameters = new List<string>();

            var argBuilder = new StringBuilder();
            var code = new StringBuilder();

            var arguments = node.Modifiers?.FirstOrDefault()?.Arguments;


            code.Append("```js");
            code.Append(Environment.NewLine);
            code.Append("constructor(");

            if (arguments == null)
            {
                return Clean(template);
            }

            foreach (var argument in arguments)
            {
                string argumentName = argument.Name;
                string dataType = argument.TypeDescriptions.TypeString.Replace("contract ", "");
                string argumentDocumentation = DocumentationHelper.Get(documentation, "param " + argumentName);

                parameters.Add(dataType + " " + argumentName);

                argBuilder.Append($"| {argumentName} | {dataType} | {Regex.Replace(argumentDocumentation, @"\r\n?|\n", " ")} | {Environment.NewLine}");
            }

            code.Append(string.Join(", ", parameters));

            code.Append(") ");

            code.Append(node.Visibility.ToString().ToLower());
            code.Append(Environment.NewLine);
            code.Append("```");

            template = template.Replace("{{ConstructorHeading}}", $"## {I18N.Constructor}");
            template = template.Replace("{{ConstructorDescription}}", description);
            template = template.Replace("{{ConstructorCode}}", code.ToString());
            template = template.Replace("{{ConstructorArguments}}", argBuilder.ToString());
            template = template.Replace("{{ConstructorArgumentsHeading}}", $"**{I18N.Arguments}**");
            template = template.Replace("{{TableHeader}}", TemplateHelper.TableHeader);

            return template;
        }

        private static string Clean(string template)
        {
            template = template.Replace("{{ConstructorDescription}}", string.Empty);
            template = template.Replace("{{ConstructorCode}}", string.Empty);
            template = template.Replace("{{ConstructorHeading}}", string.Empty);
            template = template.Replace("{{ConstructorArgumentsHeading}}", string.Empty);
            template = template.Replace("{{ConstructorArguments}}", string.Empty);
            template = template.Replace("{{TableHeader}}", string.Empty);

            return template;
        }
    }
}