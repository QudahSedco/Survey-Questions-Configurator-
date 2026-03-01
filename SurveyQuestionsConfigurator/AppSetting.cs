using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyQuestionsConfigurator
{
    internal class AppSetting
    {
        private Configuration config;

        public AppSetting()
        {
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Unexpected error");
                throw;
            }
        }

        public string GetConnectionString(string key)
        {
            try
            {
                return config.ConnectionStrings.ConnectionStrings[key].ConnectionString;
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Unexpected error");
                throw;
            }
        }

        public void SaveConnection(string connectionString, string Key)
        {
            try
            {
                config.ConnectionStrings.ConnectionStrings[Key].ConnectionString = connectionString;
                config.ConnectionStrings.ConnectionStrings[Key].ProviderName = "System.Data.SqlClient";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception tEx)
            {
                Log.Error(tEx, "Unexpected error");
                throw;
            }
        }
    }
}