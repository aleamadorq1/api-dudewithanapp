using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.Services;
using DudeWithAnApi;
using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.ServerCertificate = new X509Certificate2("./certificate.pfx");
    });

    options.Listen(IPAddress.Any, 5001);
    options.Listen(IPAddress.Any, 5002, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        listenOptions.UseHttps();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddCors();


builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IQuotePrintService, QuotePrintService>();
builder.Services.AddScoped<IQuoteTranslationService, QuoteTranslationService>();

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuotePrintRepository, QuotePrintRepository>();
builder.Services.AddScoped<IQuoteTranslationRepository, QuoteTranslationRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Skip the default logic.
                context.HandleResponse();

                // Use the HttpContext to write a response.
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                // Include the original error message in the response for debugging.
                var message = context.Error ?? "Invalid token";
                return context.Response.WriteAsync(JsonSerializer.Serialize(new { message }));
            },
                OnAuthenticationFailed = context =>
                {
                    // Log the error or return it in the response for debugging
                    Console.WriteLine(context.Exception);

                    return Task.CompletedTask;
                }

        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseRouting(); // Add this line
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints => // Update this line
{
    endpoints.MapControllers();
});

app.Run();
