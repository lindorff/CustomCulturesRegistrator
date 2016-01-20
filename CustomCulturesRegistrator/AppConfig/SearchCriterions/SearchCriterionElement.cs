using System.Configuration;

namespace CustomCulturesRegistrator.AppConfig.SearchCriterions
{
    class SearchCriterionElement : ConfigurationElement
    {
        [ConfigurationProperty("index", IsKey = true, IsRequired = true)]
        public string Index
        {
            get { return (string) base["index"]; }
            set { base["index"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string) base["value"]; }
            set { base["value"] = value; }
        }
    }
}
