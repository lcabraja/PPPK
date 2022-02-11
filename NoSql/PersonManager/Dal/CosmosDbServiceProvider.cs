using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace PersonManager.Dal
{
    public class CosmosDbServiceProvider
    {
        private const string Account = "https://pppk-6-people.documents.azure.com:443/";
        private const string Key = "GQZwhAA5qTHaVBVe9iAVrCqAdLDtdbj5FarknufWlAYU1m8frp5eLKxXvDAnIcWZDDa2HMLzDNLTMClZ6qSjvA==";
        private const string ContainerName = "People";
        private const string DatabaseName = "People";

        private static ICosmosDbService cosmosDbService;

        public static ICosmosDbService CosmosDbService { get => cosmosDbService; }

        public async static Task Init()
        {
            CosmosClient cosmosClient = new CosmosClient(Account, Key);
            cosmosDbService = new CosmosDbService(cosmosClient, DatabaseName, ContainerName);
            DatabaseResponse database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await database.Database.CreateContainerIfNotExistsAsync(ContainerName, "/id");
        }
    }
}