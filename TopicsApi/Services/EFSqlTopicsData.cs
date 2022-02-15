namespace TopicsApi.Services;

public class EFSqlTopicsData : IProvideTopicsData
{
    private readonly TopicsDataContext _context;

    public EFSqlTopicsData(TopicsDataContext context)
    {
        _context = context;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
        var data = await _context.Topics!
            .Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description)).ToListAsync();

        return new GetTopicsModel(data);
    }
}
