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

    public async Task<TopicListItemModel> AddTopicAsync(PostTopicRequestModel request)
    {
        //var topic = new Topic { Name = request.Name, Description = request.Description };
        var topic = _mapper.Map<Topic>(request); //this replaces above 
        _context.Topics!.Add(topic); // no id for the topic (the default id, 0)
        await _context.SaveChangesAsync(); // after this, it has the data base id. (Side effect, weird stuff)

        //var result = new GetTopicListItemModel(topic.Id.ToString(), topic.Name, topic.Description);
        var result = _mapper.Map<TopicListItemModel>(topic);
        return result;
    }

    public async Task<GetTopicsModel> GetAllTopics()
    {
            //.Select(t => new GetTopicListItemModel(t.Id.ToString(), t.Name, t.Description))
        var data = await GetTopics()
            .ProjectTo<TopicListItemModel>(_config)
            .ToListAsync();

        return new GetTopicsModel(data);
    }

    public async Task<Maybe<TopicListItemModel>> GetTopicByIdAsync(int topicId)
    {
        var data = await GetTopics()
            .Where(t => t.Id == topicId)
            .ProjectTo<TopicListItemModel>(_config) //this line used to be commented out line 39
            .SingleOrDefaultAsync();

        return data switch
        {
            null => new Maybe<TopicListItemModel>(false, null),
            _ => new Maybe<TopicListItemModel>(true, data),
        };
    }

    public async Task RemoveAsync(int id)
    {
        var topic = await GetTopics().Where(t => t.Id == id).SingleOrDefaultAsync();

        if (topic != null)
        {
            topic.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Maybe> ReplaceAsync(int id, TopicListItemModel request)
    {
        //does it exist? 
        var topic = await GetTopics().Where(t => t.Id == id).SingleOrDefaultAsync();
        //yes: replace it
        if (topic != null)
        {
            //copy from the request into the existing topic 
            _mapper.Map<TopicListItemModel, Topic>(request, topic);
            await _context.SaveChangesAsync();
            return new Maybe(true);
        }
        else
        {
            ////for upsert
            //topic = _mapper.Map<Topic>(request); then save to Db
            return new Maybe(false);
        }
        //no: return no 
    }
}
