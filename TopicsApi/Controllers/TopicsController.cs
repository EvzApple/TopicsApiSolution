namespace TopicsApi.Controllers;

public class TopicsController : ControllerBase
{
    //private readonly TopicsDataContext _context;

    //public TopicsController(TopicsDataContext context) 
    //{
    //    _context = context;
    //}

    private readonly IProvideTopicsData _topicsData;

    public TopicsController(IProvideTopicsData topicsData)
    {
        _topicsData = topicsData;
    }

    //NOTE: this is not needed for application - for reference only 
    //GET /topics/99 => 200 with that document or 404 
    [HttpGet("topics/{topicId:int}")]
    public async Task<ActionResult> GetTopicbyIdAsync(int topicId)
    {
        Maybe<GetTopicListItemModel> response = await _topicsData.GetTopicByIdAsync(topicId);
        //if (response == null)
        //{
        //    return NotFound();
        //}
        //else
        //{
        //    return Ok(response);
        //}
        return response.hasValue switch //this is a switch expression
        {
            false => NotFound(), //if not this, do default 
            true => Ok(response.value),//"_" denotes default condition (original code, not this code)
        };
    }

    [HttpGet("topics")]
    public async Task<IActionResult> GetTopicsAsync()
    {
        //var data = await _context.Topics!.ToListAsync();
        //return Ok(data);
        GetTopicsModel response = await _topicsData.GetAllTopics();
        return Ok(response);
    }
}
