using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Minikube.Registration.Api.Registration
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public interface IUserWriter
    {
        Task<User> SaveAsync(User user);
    }

    public interface IUserFinder
    {
        Task<Maybe<User>> FindByEmail(string email);
    }

    public class UserStorage : IUserWriter, IUserFinder
    {
        private const string DbName = "registration";
        private const string CollectionName = "users";
        private readonly MongoClient _client;

        public UserStorage(MongoClient client)
        {
            _client = client;
        }

        private async Task CreateCollectionIfNotExists(IMongoDatabase db)
        {
            var collections = await (await db.ListCollectionNamesAsync()).ToListAsync();
            if (!collections.Any(c => c == CollectionName))
            {
                await db.CreateCollectionAsync(CollectionName);
            }
        }

        private async Task<IMongoCollection<User>> InitCollection()
        {
            var db = _client.GetDatabase(DbName);
            await CreateCollectionIfNotExists(db);
            return db.GetCollection<User>(CollectionName);
        }

        public async Task<User> SaveAsync(User user)
        {
            var users = await InitCollection();
            user.Id = Guid.NewGuid();
            users.InsertOne(user);

            return user;
        }

        public async Task<Maybe<User>> FindByEmail(string email)
        {
            var users = await InitCollection();
            var filter = new FilterDefinitionBuilder<User>().Where(u => u.Email == email);
            using (var cursor = await users.FindAsync(filter))
            {
                return await cursor.MoveNextAsync()
                    ? new Maybe<User>(cursor.Current.FirstOrDefault())
                    : new Maybe<User>("User not found");
            }
        }
    }
}