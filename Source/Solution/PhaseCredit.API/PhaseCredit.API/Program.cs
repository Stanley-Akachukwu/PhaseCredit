using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using System.Text.Json;
using PhaseCredit.API.Pipelines;
using PhaseCredit.Core.BusinessLogic.Authentication;
using PhaseCredit.Core.Services.Reservations;
using PhaseCredit.Core.Services.Users;
using PhaseCredit.Core.Services.Logs;
using PhaseCredit.API.Common;

var builder = WebApplication.CreateBuilder(args);
//var identityServer = builder.Configuration.GetConnectionString("IdentityServer:Url");
var identityServer = builder.Configuration["IdentityServer:Url"];

// Add services to the container. 

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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = identityServer;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.SaveToken = true;
//        options.RequireHttpsMetadata = false;
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidAudience = "https://www.phasecredit.com",
//            ValidIssuer = "https://www.phasecredit.com",
//            ClockSkew = TimeSpan.Zero,// It forces tokens to expire exactly at token expiration time instead of 5 minutes later
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phasecredit-for-soledealler01"))
//        };
//    });

builder.Services.AddScoped<IAppSettings, AppSettings>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
