using Common.Message.Events;
using Microsoft.Data.SqlClient;
using NServiceBus;
using Saga.Message;
using Saga.Message.Saga;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var hostBuilder = builder.Host.ConfigureDefaults(args);
hostBuilder.UseNServiceBus(context =>
{
    var friendlyName = "NimaOutboxPattern";
    var endpointConfiguration = new EndpointConfiguration(friendlyName);
    var connection = builder.Configuration.GetSection("SagaConnectionString").Value;
    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    var subscriptions = persistence.SubscriptionSettings();

    subscriptions.CacheFor(TimeSpan.FromMinutes(1));
    persistence.SqlDialect<SqlDialect.MsSqlServer>();
    persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(connection);
        });

    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.UseDirectRoutingTopology();
    string nserviceBusConnection = builder.Configuration.GetSection("NServiceBusConnectionString").Value;
    transport.ConnectionString(nserviceBusConnection);

    return endpointConfiguration;
});

builder.Services.AddSingleton<IBus, Bus>();
// Add sagas
builder.Services.AddScoped<IHandleMessages<FirstEvent>, FirstSaga>();

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
