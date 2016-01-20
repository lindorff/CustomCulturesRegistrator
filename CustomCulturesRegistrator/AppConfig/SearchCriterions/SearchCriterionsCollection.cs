using System.Configuration;

namespace CustomCulturesRegistrator.AppConfig.SearchCriterions
{
    public class SearchCriterionsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SearchCriterionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SearchCriterionElement)element).Index;
        }
    }
}
