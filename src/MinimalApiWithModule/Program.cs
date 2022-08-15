using MinimalApiWithModule.Modules;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(c =>
{
	string? envDir = Environment.CurrentDirectory;
	string DatabasePath = $"{envDir}{Path.DirectorySeparatorChar}Ticketing.db";
	c.UseSqlite($"Filename={DatabasePath}");
});
builder.Services.RegisterModules();
builder.Services.AddScoped<ILogger, Logger<Program>>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "MinimalApiWithModule", Version = "v1" });
	string? docFilePath = Path.Combine(System.AppContext.BaseDirectory, "MinimalApiWithModule.xml");
	c.IncludeXmlComments(docFilePath);
});

WebApplication app = builder.Build();

app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapSwagger();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapEndpoints();
app.UseRouting();
app.UseAuthorization();

app.Logger.LogInformation("Starting MinimalApiWithModule {date}", DateTime.UtcNow);
app.Run();

/// <summary>Program class.</summary>
/// <remarks>Make the implicit Program class public so test projects can access it.</remarks>
public partial class Program { }