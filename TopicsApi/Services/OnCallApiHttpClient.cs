namespace TopicsApi.Services;

public class OnCallApiHttpClient
{
    private readonly HttpClient _httpClient;
    public OnCallApiHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient; //NEVER EVER call "new" on the HttpClient
    }
    public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync()
    {
        //call into the other service, and just return whatever that sucker returns from here 
        var response = await _httpClient.GetAsync("/");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadFromJsonAsync<GetCurrentDeveloperModel>();

        return content!;
    }
}
