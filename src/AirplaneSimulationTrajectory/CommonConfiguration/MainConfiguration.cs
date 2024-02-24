using CommonConfiguration.Configuration;
using System.IO;
using System;
using CommonConfiguration.Configuration.Model;

namespace CommonConfiguration
{
    public class MainConfiguration
    {
        private const string HardwareSettings = "configuration.xml";

        public Settings GetSettings()
        {
           return XmlSerializerWithoutNamespaces.DeserializeFromStream<Settings>(ReadSettingsFromFile());
        }

        private static string ReadSettingsFromFile()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HardwareSettings);
        }
    }
}