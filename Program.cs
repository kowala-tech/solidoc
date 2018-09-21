using System.IO;
using System.Linq;
using Solidoc.Serializers;
using Solidoc.Utility;

namespace Solidoc
{
    public class Program
    {
        internal static void Main(string[] args)
        {
            ResourceWriter.Run();

            if (args.Length > 2)
            {
                ConsoleUtility.WriteException(string.Format(I18N.InvalidCommand, string.Join(" ", args)));
                return;
            }

            string pathToBuildDirectory = args[0];
            string outputPath = args[1];

            var directory = new DirectoryInfo(outputPath);

            if (!directory.Exists)
            {
                directory.Create();
            }

            var parser = new ContractParser(pathToBuildDirectory);
            var contracts = parser.Parse();

            var generator = new Serializer(contracts.ToList(), outputPath);
            generator.Serialize();

            //Console.ReadLine();
        }
    }
}