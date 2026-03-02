using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator
{
    /// <summary>
    /// Helper class to read and update application database connection strings, using the application's
    /// configuration file App.config
    /// </summary>
    internal class AppSetting
    {
        private Configuration config;

        /// <summary>
        /// Initializes a new instance of the AppSetting class
        /// and loads the application configuration file.
        /// </summary>
        public AppSetting()
        {
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Retrieves the connection string for the specified key from the configuration.
        /// </summary>
        /// <param name="pKey">The key/name of the connection string in the config file.</param>
        /// <returns>The connection string associated with the specified key.</returns>
        public string GetConnectionString(string pKey)
        {
            try
            {
                return config.ConnectionStrings.ConnectionStrings[pKey].ConnectionString;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates the connection string for the specified key and saves it to the configuration file.
        /// </summary>
        /// <param name="pConnectionString">The new connection string to save.</param>
        /// <param name="pKey">The key/name of the connection string to update.</param>
        public void SaveConnection(string pConnectionString, string pKey)
        {
            try
            {
                config.ConnectionStrings.ConnectionStrings[pKey].ConnectionString = pConnectionString;
                config.ConnectionStrings.ConnectionStrings[pKey].ProviderName = "System.Data.SqlClient";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, tEx.Message);
                throw;
            }
        }
    }
}