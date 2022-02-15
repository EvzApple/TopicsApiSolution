using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TopicsApi.Services;

public class EFSqlTopicsData : IProvideTopicsData
{
    private readonly TopicsDataContext _context;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _config;

    public EFSqlTopicsData(TopicsDataContext context, IMapper mapper, MapperConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
    }

    private IQueryable<Topic> GetTopics() //this is pre-filtering TOpics, allows us to get Topics that are active
    {
        return _context.Topics!
            .Where(t => t.IsDeleted == false);
    }

    public async Task<GetTopicListItemModel> AddTopicAsync(PostTopicRequestModel request)
    {
        //var topic = new Topic { Name = request.Name, Description = request.Description };
        var topic = _mapper.Map<Topic>(request); //this replaces above 
        _context.Topics!.Add(topic); // no id for the topic (the default id, 0)
        await _context.SaveChangesAsync(); // after this, it has the data base id. (Side effect, weird stuff)

        //var result = new GetTopicListItemModel(topic.Id.ToString(), topic.Name, topic.Description);
        var result = _mapper.Map<GetTopicListItemModel>(topic);
        return result;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
            //.Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description))
        var data = await GetTopics()
            .ProjectTo<GetTopicListItemModel>(_config)
            .ToListAsync();

        return new GetTopicsModel(data);
    }

    public async Task<Maybe<GetTopicListItemModel>> GetTopicByIdAsync(int topicId)
    {
        var data = await GetTopics()
            .Where(t => t.Id == topicId)
            .ProjectTo<GetTopicListItemModel>(_config) //this line used to be commented out line 39
            .SingleOrDefaultAsync();

        return data switch
        {
            null => new Maybe<GetTopicListItemModel>(false, null),
            _ => new Maybe<GetTopicListItemModel>(true, data),
        };
    }
}
