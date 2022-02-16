namespace TopicsApi.Services
{
    public class RPCDeveloperLookup : ILookupOnCallDevelopers
    {
        private readonly OnCallApiHttpClient _client;

        public RPCDeveloperLookup(OnCallApiHttpClient client)
        {
            _client = client;
        }

        public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDeveloperAsync()
        {
            try
            {
                return await _client.GetCurrentOnCallDeveloperAsync();
            }
            catch (Exception)
            {
                return new GetCurrentDeveloperModel("Unable to provide the current On Call Developer",
                    "800-get-help", "help@company.com", DateTime.Now);
            }
        }
    }
}
