namespace TopicsApi.Services
{
    public interface IProvideTopicsData
    {
        Task<GetTopicsModel> GetAllTopics();
        Task<Maybe<TopicListItemModel>> GetTopicByIdAsync(int topicId);
        Task<TopicListItemModel> AddTopicAsync(PostTopicRequestModel request);
        Task RemoveAsync(int id);
        Task<Maybe> ReplaceAsync(int id, TopicListItemModel request);
    }
}
