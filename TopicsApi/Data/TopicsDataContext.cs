using Microsoft.EntityFrameworkCore;

namespace TopicsApi.Data;

public class TopicsDataContext : DbContext
{
    public TopicsDataContext(DbContextOptions<TopicsDataContext> options) :base(options)
    {

    }
    public DbSet<Topic>? Topics { get; set;}
}
