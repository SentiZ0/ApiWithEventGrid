using ApiWithEventGrid.Options;
using ApiWithEventGrid.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<EventGridService>();
builder.Services.Configure<EventGridOptions>(
    builder.Configuration.GetSection("EventGrid"));

builder.Services.AddHttpClient("eventgrid", (serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<EventGridOptions>>().Value;
    client.BaseAddress = new Uri(options.TopicEndpoint);
    client.DefaultRequestHeaders.Add("aeg-sas-key", options.TopicKey);
});

builder.Services.AddSwaggerGen();

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
