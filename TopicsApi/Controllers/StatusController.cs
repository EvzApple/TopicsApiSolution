
namespace TopicsApi.Controllers;

public class StatusController :ControllerBase
{
    private readonly ILookupOnCallDevelopers _onCallDeveloperLookup;

    public StatusController(ILookupOnCallDevelopers onCallDeveloperLookup)
    {
        _onCallDeveloperLookup = onCallDeveloperLookup;
    }

    [HttpGet("status/on-call-developer")] 
    public async Task<ActionResult<GetCurrentDeveloperModel>> GetOnCallDeveloperAsync()
    {
        GetCurrentDeveloperModel response = await _onCallDeveloperLookup.GetCurrentOnCallDeveloperAsync();
        return Ok(response);
    }
}
