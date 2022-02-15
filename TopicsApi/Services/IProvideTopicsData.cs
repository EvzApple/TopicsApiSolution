namespace TopicsApi.Services
{
    public interface IProvideTopicsData
    {
        Task<GetTopicsModel> GetAllTopics();
        Task<Maybe<GetTopicListItemModel>> GetTopicByIdAsync(int topicId);
    }
}
