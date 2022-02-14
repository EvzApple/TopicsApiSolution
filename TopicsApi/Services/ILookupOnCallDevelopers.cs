namespace TopicsApi.Services;

public interface ILookupOnCallDevelopers
{
    Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync();
}
