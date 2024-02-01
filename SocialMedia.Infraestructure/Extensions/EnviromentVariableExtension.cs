using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Text;

namespace SocialMedia.Infraestructure.Extensions
{
    public static class EnviromentVariableExtension
    {
        public static string GetFromEnviroment(this IConfiguration configuration, string name)
        {
            var variables = Environment.GetEnvironmentVariables();
            string connectionString = configuration.GetConnectionString(name) ?? string.Empty;
            StringBuilder stringBuilder = new StringBuilder(connectionString);
            foreach (DictionaryEntry variable in variables)
            {
                if(connectionString.Contains(variable.Key.ToString()))
                {
                    stringBuilder.Replace(variable.Key.ToString(), variable.Value.ToString());
                }
            }

            return stringBuilder.ToString();
        }
    }
}
