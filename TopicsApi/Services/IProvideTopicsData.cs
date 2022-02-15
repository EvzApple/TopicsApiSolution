namespace TopicsApi.Services
{
    public interface IProvideTopicsData
    {
        Task<GetTopicsModel> GetAllTopics();
    }
}
