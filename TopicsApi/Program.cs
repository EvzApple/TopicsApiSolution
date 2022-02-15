var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//configuration of the backing services.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(config => //this sets up rules for CORS 
{
    config.AddDefaultPolicy(pol =>
    {
        pol.AllowAnyOrigin();
        pol.AllowAnyMethod();
        pol.AllowAnyHeader(); //You can't allow this AND AllowCredentials
    });
});

builder.Services.AddTransient<ILookupOnCallDevelopers, FakeDeveloperLookup>();
builder.Services.AddScoped<IProvideTopicsData, EFSqlTopicsData>();

//The TopicsDataContext is set up as a Scoped Service. You can inject it into your controllers, services, and stuff
builder.Services.AddDbContext<TopicsDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("topics"));
});

//building the actual application 
var app = builder.Build();

app.UseCors(); //handles the OPTIONS request from browsers by using the services we set above 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run(); //blocking call 
