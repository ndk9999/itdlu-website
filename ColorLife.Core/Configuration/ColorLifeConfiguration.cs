using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ColorLife.Core.Configuration
{
    /// <summary>
    /// Demo tuanitpro
    /// </summary>
    public class ColorLifeElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }
        [ConfigurationProperty("cache", IsRequired = false, DefaultValue = false)]
        public bool Cache
        {
            get { return (bool)this["cache"]; }
            set { this["cache"] = value; }
        }
    }
    public class ColorLifeRetrieverSection : ConfigurationSection
    {
        [ConfigurationProperty("colorLifeSettings", IsDefaultCollection = true)]
        public ColorLifeCollection colorLifeSettings
        {
            get { return (ColorLifeCollection)this["colorLifeSettings"]; }
            set { this["colorLifeSettings"] = value; }
        }
    }
    [ConfigurationCollection(typeof(ColorLifeElement))]
    public class ColorLifeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ColorLifeElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ColorLifeElement)element).Name;
        }
    }
}
