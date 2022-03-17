using CosmosDbTest.Model;

namespace CosmosDbTest.Services
{
    public interface IVideoGameService
    {
        Task<IEnumerable<VideoGame>> GetAllAsync();
        Task SaveAsync(VideoGame item);
        Task<VideoGame> GetById(string id);

        Task<VideoGame> Put(VideoGame item);

        Task<VideoGame> DeleteById(string id);

        Task<string> GetName(string id);

    }
}