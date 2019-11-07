using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Minikube.Registration.Api.Probe
{
    public interface IStorageAccessibilityChecker
    {
        Task<bool> CheckAccessibility();
    }

    public class StorageAccessibilityChecker : IStorageAccessibilityChecker
    {
        private const string DbName = "registration";
        private const string CollectionName = "users";

        private readonly MongoClient _client;

        public StorageAccessibilityChecker(MongoClient client)
        {
            _client = client;
        }

        public async Task<bool> CheckAccessibility()
        {
            try
            {
                var db = _client.GetDatabase(DbName);
                var collections = await (await db.ListCollectionNamesAsync()).ToListAsync();
                if (!collections.Any(c => c == CollectionName))
                {
                    await db.CreateCollectionAsync(CollectionName);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}