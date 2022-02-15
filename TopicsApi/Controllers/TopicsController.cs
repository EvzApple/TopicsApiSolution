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

    [HttpGet("topics")]
    public async Task<IActionResult> GetTopicsAsync()
    {
        //var data = await _context.Topics!.ToListAsync();
        //return Ok(data);
        GetTopicsModel response = await _topicsData.GetAllTopics();
        return Ok(response);
    }
}
