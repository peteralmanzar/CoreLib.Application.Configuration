using System;
using System.Configuration;

namespace CoreLib.Application.Configuration
{
    /// <summary>
    /// Provides methods to encrypt .NET application configurations.
    /// </summary>
    public static class ConfigurationProtectionService
    {
        #region Public Methods
        /// <summary>
        /// Encrypts specified configuration section.
        /// </summary>
        /// <param name="sectionName">Name of section to encrypt.</param>
        public static void ProtectConfiguration(string sectionName)
        {
            if(string.IsNullOrEmpty(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configSectionObject = config.GetSection(sectionName);

            if(configSectionObject is ConfigurationSection configurationSection)
                if(!configurationSection.IsReadOnly() && !configurationSection.SectionInformation.IsProtected)
                {
                    configurationSection.SectionInformation.ProtectSection(null);
                    config.Save(ConfigurationSaveMode.Full);
                }
        }

        /// <summary>
        /// Decrypts specified configuration section
        /// </summary>
        /// <param name="sectionName">Name of section to encrypt.</param>
        public static void UnprotectConfiguration(string sectionName)
        {
            if(string.IsNullOrEmpty(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var configSectionObject = config.GetSection(sectionName);

            if(configSectionObject is ConfigurationSection configurationSection)
                if(!configurationSection.IsReadOnly() && configurationSection.SectionInformation.IsProtected)
                {
                    configurationSection.SectionInformation.UnprotectSection();
                    config.Save(ConfigurationSaveMode.Full);
                }
        }

        /// <summary>
        /// Encrypt 
        /// </summary>
        public static void ProtectConnectionStrings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if(!config.ConnectionStrings.SectionInformation.IsProtected)
            {
                config.ConnectionStrings.SectionInformation.ProtectSection(null);
                config.Save(ConfigurationSaveMode.Full, true);
            }
        }

        public static void UnprotectConnectionStrings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if(config.ConnectionStrings.SectionInformation.IsProtected)
            {
                config.ConnectionStrings.SectionInformation.UnprotectSection();
                config.Save(ConfigurationSaveMode.Full, true);
            }
        }
        #endregion
    }
}
