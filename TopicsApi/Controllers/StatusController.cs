
namespace TopicsApi.Controllers;

public class StatusController :ControllerBase
{
    [HttpGet("status/oncalldeveloper")] 
    public ActionResult GetOnCallDeveloper()
    {
        return Ok();
    }
}
