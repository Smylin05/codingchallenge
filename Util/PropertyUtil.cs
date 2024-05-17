using System;

using System.IO;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace HosManagement.Util
{
    public static class PropertyUtil
    {
        private static SqlConnection connection;

        public static string GetPropertyString()
        {
            string propertyFilePath = "D:\\HosManagement\\HosManagement\\HosManagement\\appsettings.json";

            string server = "DESKTOP-U9L54I2";
            string dbname = "HospitalManagement";
            string username = "smylin";
            string password = "christabell123$";
            string trustedconnection = "true";

            try
            {
                if (File.Exists(propertyFilePath))
                {
                    string json = File.ReadAllText(propertyFilePath);
                    dynamic jsonData = JsonConvert.DeserializeObject(json);

                    server = jsonData["Server"];
                    dbname = jsonData["Database"];
                    username = jsonData["Username"];
                    password = jsonData["Password"];
                    trustedconnection = jsonData["TrustServerCertificate"];
                }
                else
                {
                    Console.WriteLine("JSON property file not found. Using default connection details.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON property file: " + ex.Message);
            }

            // This is where you construct your connection string
            string connectionString = $"Server={server};Database={dbname};User Id={username};Password={password}; TrustServerCertificate ={trustedconnection}";
            Console.WriteLine($"Connection Strings:{connectionString}");
            return connectionString;
        }




    }
}
