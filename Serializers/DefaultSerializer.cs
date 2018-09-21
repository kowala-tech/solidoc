using System;
using System.Collections.Generic;
using Solidoc.Models;

namespace Solidoc.Serializers
{
    public sealed class DefaultSerializer : ISerializer
    {
        public string Serialize(Contract contract, string template, List<Contract> contracts)
        {
            template = template.Replace("{{Time}}", DateTime.Now.ToString("T"));
            template = template.Replace("{{Date}}", DateTime.Now.ToString("u"));
            template = template.Replace("{{CurrentDirectory}}", Environment.CurrentDirectory);

            return template;
        }
    }
}