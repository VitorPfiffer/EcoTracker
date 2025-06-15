using EcoTracker.Application.Profiles;
using EcoTracker.Core.Api.Authorization;
using EcoTracker.Core.Api.Serilog;
using EcoTracker.Core.AutoMapper;
using EcoTracker.Core.Extensions;
using EcoTracker.Core.FluentValidator;
using EcoTracker.Core.Infrastructure.Extensions;
using EcoTracker.Core.Refit;
using EcoTracker.Core.ServicesInjection;
using EcoTracker.Core.Swagger.Extensions;
using EcoTracker.Infrastructure.Context;
using EcoTracker.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContext<EcoTrackerContext, EcoTrackerUnitOfWork>("EcoTracker");

builder.AddSerilog();

builder.AddJwtAuthentication("JWT:ApiSecret");

var services = builder.Services;

services.AddAutoMapper<MappingProfiles>();

services.AddValidators();

services.AddServices();

services.AddWebApi();

services.AddSwaggerDocumentation();

services.AddMemoryCache();

services.AddNotification("Messages.ApplicationErrorMessages");




var app = builder.Build();

app.UseSerilog();

app.UseIoC();

app.UseSwaggerDocumentation();

app.UseAuthentication();

app.UseAuthorization();

app.UseWebApi();

app.UseApiClient();

app.Run();