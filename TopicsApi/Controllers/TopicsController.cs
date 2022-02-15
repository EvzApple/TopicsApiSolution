namespace TopicsApi.Controllers;

[ApiController]
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

    [HttpPost("topics")]
    public async Task<ActionResult> AddTopicAsync([FromBody] PostTopicRequestModel request)
    {
        //1. Validate incoming data
        //2. If invalid
        //if (!ModelState.IsValid)m
        //{
        //    // a) send a 400 (this is usually all we do)
        //    return BadRequest(ModelState);
        //}
        // b) you can be "nice" and tell them what they did wrong lol
        //3. If valid,
        // a) do the work (side effect); for us, add it to database, etc.
        // b) return a 201 Created status code
        // add a location header to the response with the URI of the new resource (Location: http://localhost:1337/topics/3)
        // Maybe just give them a copy of what they'd get from that URI
        //201 SHOULD have a Location Header and a copy of the object that was created 

        GetTopicListItemModel response = await _topicsData.AddTopicAsync(request);

        return CreatedAtRoute("topics.getbyidasync", new { topicId = response.id }, response); 
    }

    //NOTE: this is not needed for application - for reference only 
    //GET /topics/99 => 200 with that document or 404 
    [HttpGet("topics/{topicId:int}", Name ="topics.getbyidasync")]
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
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<IActionResult> GetTopicsAsync()
    {
        //var data = await _context.Topics!.ToListAsync();
        //return Ok(data);
        GetTopicsModel response = await _topicsData.GetAllTopics();
        return Ok(response);
    }
}
