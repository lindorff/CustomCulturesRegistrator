using System.Configuration;

namespace CustomCulturesRegistrator.AppConfig.SearchCriterions
{
    public class SearchCriterionsSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public SearchCriterionsCollection Instances
        {
            get { return (SearchCriterionsCollection) this[""]; }
            set { this[""] = value; }
        }
    }
}
