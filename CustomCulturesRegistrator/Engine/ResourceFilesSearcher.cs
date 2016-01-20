using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CustomCulturesRegistrator.Engine
{
    class ResourceFilesSearcher
    {
        private static IEnumerable<string> SearchFilesFilesInDirectory(string path, string creteria)
        {
            var result = new List<string>();

            result.AddRange(Directory.EnumerateFiles(path, creteria));
            result.AddRange(Directory.EnumerateDirectories(path).SelectMany(d => SearchFilesFilesInDirectory(d, creteria)));

            return result;
        }

        public static string[] SearchResourcesInDirectory(string path, string[] criterions)
        {
            var result = criterions.SelectMany(e => SearchFilesFilesInDirectory(path, e));

            return result.Select(Path.GetFileName).ToArray();
        }

        public static string[] GetCulturesFromResourcesFileNames(string[] resourceFilesNames)
        {
            var cultureCodes = resourceFilesNames.Select(GetMatch).Where(e => !string.IsNullOrEmpty(e));
            return cultureCodes.ToArray();
        }

        private static string GetMatch(string resourceName)
        {
            var result = Regex.Match(resourceName, @"(?<=\.)[\w]{2}(-[\w]{2})?(?=\.)");
            return result.Value;
        }
    }
}
