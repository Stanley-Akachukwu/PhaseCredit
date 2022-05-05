using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using System.Text.Json;
using PhaseCredit.API.Pipelines;
using PhaseCredit.Core.Services.Reservations;
using PhaseCredit.Core.Services.Users;
using PhaseCredit.Core.Services.Logs;
using PhaseCredit.API.Common;
using PhaseCredit.Core.Services.Authentications;
using PhaseCredit.Core.Services.ClientAuthorization;

var builder = WebApplication.CreateBuilder(args);
//var identityServer = builder.Configuration.GetConnectionString("IdentityServer:Url");
//var identityServer = builder.Configuration["IdentityServer:Url"];
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddMediator(o =>
{
    o.AddPipeline<LoggingPipeline>();
    //o.AddPipeline<TimeoutPipeline>();
    o.AddPipeline<ValidationPipeline>();
    //o.AddPipeline<TransactionPipeline>();
    //o.AddPipeline<AuditPipeline>();

    foreach (var implementationType in typeof(Program)
        .Assembly
        .ExportedTypes
        .Where(t => t.IsClass && !t.IsAbstract))
    {
        foreach (var serviceType in implementationType
            .GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)))
        {
            o.Services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient));
        }
    }

    o.AddHandlersFromAssemblyOf<Program>();
});

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer(options =>
//    {
//        options.Authority = identityServer;
//        options.TokenValidationParameters.ValidateAudience = false;
//    });
//builder.Services.AddAuthorization(options =>
//    options.AddPolicy("ApiScope", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireClaim("scope", "phaseCreditAPI");
//    })
//);


builder.Services.AddScoped<IAppSettings, AppSettings>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientAuthorization, ClientAuthorizationService>();
builder.Services.AddScoped<ILogService, LogService>();

var app = builder.Build();

// Configure the HTTP request pipeline.  : 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (ValidationException e)
    {
        var response = ctx.Response;
        if (response.HasStarted)
            throw;

        ctx.RequestServices
            .GetRequiredService<ILogger<Program>>()
            .LogError(e.Message, e.StackTrace);

        response.Clear();
        response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        await response.WriteAsync(JsonSerializer.Serialize(new
        {
            Message = "Invalid data has been submitted22",
            ModelState = e.Errors.ToDictionary(error => error.ErrorCode, error => error.ErrorMessage)
        }), Encoding.UTF8, ctx.RequestAborted);
    }
});
app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers().RequireAuthorization("ApiScope");
app.MapControllers();

app.Run();
