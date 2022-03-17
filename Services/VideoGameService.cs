using CosmosDbTest.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;


namespace CosmosDbTest.Services
{
    public class VideoGameService : IVideoGameService
    {
        // chave de conexao com o cosmosDb
        private const string _connectionString = "<>";
        // definindo qual banco de dados
        private const string _dataBaseId = "Competition";
        // selecionando o container
        private const string _containerId = "VideoGames1";
        //metodos para interair com o cosmos
        private readonly Container _container;

        public VideoGameService()
        {
            //acessando a chave de conexao
            var client = new CosmosClientBuilder(_connectionString).Build();
            // acessando o banco de dados e container
            _container = client.GetContainer(_dataBaseId, _containerId);
        }
        public async Task<IEnumerable<VideoGame>> GetAllAsync()
        {
            // acesse o banco de dados e pegue todos os item retornando numa lista. 
            return _container.GetItemLinqQueryable<VideoGame>(allowSynchronousQueryExecution: true).ToList();
            /* var queryDefinition = "SELECT * FROM c";

            List<VideoGame> results = new List<VideoGame>();

            var query = this._container.GetItemQueryIterator<VideoGame>(new QueryDefinition(queryDefinition));

            while (query.HasMoreResults){
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            } */
        }

        public async Task SaveAsync(VideoGame item)
        {
            //crie um novo item no container
            await _container.CreateItemAsync<VideoGame>(item, new PartitionKey(item.Id));
        }

        public async Task<VideoGame> GetById(string id)
        {
            // buscar um item atraves de seu Id
            return await _container.ReadItemAsync<VideoGame>(id, new PartitionKey(id));
        }

        public async Task<VideoGame> DeleteById(string id)
        {
            // Deletar um item atraves de seu Id
            return await _container.DeleteItemAsync<VideoGame>(id, new PartitionKey(id));
        }

        public async Task<VideoGame> Put(VideoGame item)
        {
            // fazer uma alteracao num dado existente
            return await _container.ReplaceItemAsync<VideoGame>(item, item.Id, new PartitionKey(item.Id));
        }

        public async Task<string> GetName(string id)
        {
            // buscar o nome do jogo atraves do Id.
            var q = _container.GetItemLinqQueryable<VideoGame>(allowSynchronousQueryExecution: true)
            .Where(x => x.Id == id)
            .Select(s => s.Name)
            .AsEnumerable()
            .First();

            return $"Nome do Jogo: {q}";
        }
    }
}
