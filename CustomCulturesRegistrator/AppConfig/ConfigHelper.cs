using System.Linq;
using System.Configuration;
using CustomCulturesRegistrator.AppConfig.SearchCriterions;

namespace CustomCulturesRegistrator.AppConfig
{
    public class ConfigHelper
    {
        private static string[] _searchCreterions;

        public static string[] SearchCreterions
        {
            get { return _searchCreterions ?? (_searchCreterions = GetSearchCriterions()); }
        }

        private static string[] GetSearchCriterions()
        {
            var searchCriterionsSection = (SearchCriterionsSection) ConfigurationManager.GetSection("SearchCriterions");
            var result = searchCriterionsSection.Instances.Cast<SearchCriterionElement>().Select(e => e.Value).ToArray();
            
            return result;
        }
    }
}
