using CloudCustomers.API.Config;
using CloudCustomers.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    services.AddScoped<IUserService, UserService>();
    services.AddHttpClient<IUserService, UserService>();

    services.Configure<UserApiOptions>(
        builder.Configuration.GetSection("UserApiOptions"));
}
