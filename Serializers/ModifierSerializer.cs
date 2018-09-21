using System;
using System.Collections.Generic;
using System.Linq;
using Solidoc.Builders;
using Solidoc.Models;
using Solidoc.Utility;

namespace Solidoc.Serializers
{
    public sealed class ModifierSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            var modifierNodes = NodeHelper.GetModifiers(contract).ToList();

            if (!modifierNodes.Any())
            {
                return Clean(template);
            }

            var definitionList = new List<string>();
            var modifierList = modifierNodes.Select(node => $"- [{node.Name}](#{node.Name.ToLower()})").ToList();


            template = template.Replace("{{ModifierTitle}}", $"## {I18N.Modifiers}");

            foreach (var node in modifierNodes)
            {
                string modifierTemplate = TemplateHelper.Modifier;
                string documentation = node.Documentation;
                string description = DocumentationHelper.GetNotice(documentation);

                var argumentBuilder = new ArgumentBuilder(documentation, node.Parameters.Parameters);
                var codeBuilder = new ModifierCodeBuilder(node);

                modifierTemplate = modifierTemplate.Replace("{{ModifierArgumentsHeading}}", $"**{I18N.Arguments}**");
                modifierTemplate = modifierTemplate.Replace("{{TableHeader}}", TemplateHelper.TableHeader);
                modifierTemplate = modifierTemplate.Replace("{{ModifierNameHeading}}", $"### {node.Name}");
                modifierTemplate = modifierTemplate.Replace("{{ModifierDescription}}", description);
                modifierTemplate = modifierTemplate.Replace("{{ModifierCode}}", codeBuilder.Build());
                modifierTemplate = modifierTemplate.Replace("{{ModifierArguments}}", argumentBuilder.Build());

                definitionList.Add(modifierTemplate);
            }

            template = template.Replace("{{ModifierList}}", string.Join(Environment.NewLine, modifierList));
            template = template.Replace("{{AllModifiers}}", string.Join(Environment.NewLine, definitionList));

            return template;
        }

        private static string Clean(string template)
        {
            template = template.Replace("{{ModifierTitle}}", string.Empty);
            template = template.Replace("{{ModifierList}}", string.Empty);
            template = template.Replace("{{AllModifiers}}", string.Empty);

            return template;
        }
    }
}