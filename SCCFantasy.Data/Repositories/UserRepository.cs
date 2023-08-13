using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using SCCFantasy.Common.Models;
using SCCFantasy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Data.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserById(string id);
        Task<UserEntity> GetUserByName(string userName);
        Task<bool> CheckIfUserNameExist(string userName);
        Task<UserEntity> AddUser(UserEntity entity);
        Task<UserEntity> UpdateUser(UserEntity entity);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly string ContainerName = "User";
        private readonly Container _container;

        public UserRepository(CosmosClient cosmosClient) : base()
        {
            Database database = cosmosClient.GetDatabase(this.DatabaseName);
            _container = database.GetContainer(ContainerName);
        }

        public async Task<UserEntity> AddUser(UserEntity entity)
        {
            var result = await _container.CreateItemAsync(entity, new PartitionKey(entity.id));
            return result.Resource;
        }

        public async Task<UserEntity> UpdateUser(UserEntity entity)
        {
            var result = await _container.UpsertItemAsync(entity, new PartitionKey(entity.id));
            return result.Resource;
        }

        public async Task<bool> CheckIfUserNameExist(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            var user = await GetUserByName(userName);

            return user != null;
        }

        public async Task<UserEntity> GetUserByName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return null;
            }

            var listUser = new List<UserEntity>();

            FeedIterator<UserEntity> feedIterator = _container.GetItemLinqQueryable<UserEntity>()
            .Where(x => x.UserName.ToLower() == userName.ToLower()).ToFeedIterator();

            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    {
                        listUser.Add(item);
                    }
                }
            }

            return listUser.FirstOrDefault(x => x.UserName == userName);
        }

        public async Task<UserEntity> GetUserById(string id)
        {
            var itemResponse = await _container.ReadItemAsync<UserEntity>(id, new PartitionKey(id));

            return itemResponse.Resource;
        }
    }
}
