using Microsoft.Extensions.Configuration;

namespace Minikube.Registration.Configuration
{
    public class StorageConfig
    {
        public string ConnectionString { get; }

        public StorageConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static StorageConfig Load(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("Storage:ConnectionString");

            return new StorageConfig(connectionString);
        }
    }
}