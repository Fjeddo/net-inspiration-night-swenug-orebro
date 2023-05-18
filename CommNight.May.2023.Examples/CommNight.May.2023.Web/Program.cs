using CommNight.May._2023.CodeContainers;
using CommNight.May._2023.Domain;
using CommNight.May._2023.Domain.Data;
using CommNight.May._2023.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<Dispatcher>();
builder.Services.AddSingleton<ParkingTicketRepository>();
builder.Services.AddSingleton<SocialSecurityNumberHelper>();
builder.Services.AddSingleton<RegNrFormatter>();
builder.Services.AddSingleton<RegNrValidator>();
builder.Services.AddSingleton<ParkingTicketService>();
builder.Services.AddSingleton<ICommandHandler<CommNight.May._2023.Domain.Features.IssueParkingTicket.V1.IssueParkingTicketCommand>, CommNight.May._2023.Domain.Features.IssueParkingTicket.V1.IssueParkingTicketCommandHandler>();
builder.Services.AddSingleton<ICommandHandler<CommNight.May._2023.Domain.Features.IssueParkingTicket.V2.IssueParkingTicketCommand>, CommNight.May._2023.Domain.Features.IssueParkingTicket.V2.IssueParkingTicketCommandHandler>();
builder.Services.AddLogging();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
