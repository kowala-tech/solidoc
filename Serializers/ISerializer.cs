using System.Collections.Generic;
using Solidoc.Models;

namespace Solidoc.Serializers
{
    public interface ISerializer
    {
        string Serialize(Contract contract, string template, List<Contract> contracts);
    }
}