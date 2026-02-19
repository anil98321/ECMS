using ECMS.Core.DependencyConfiguration;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCampaignService(builder.Configuration);
builder.Services
    .AddFastEndpoints(f =>
    {
        f.Assemblies = [typeof(ServiceRegistrations).Assembly, typeof(Program).Assembly];
    }).SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.DocumentName = "v1";
            s.Title = "Email Campaign Management System";
            s.Version = "v1";
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api/v1";
});

app.UseSwaggerGen();
app.UseSwaggerUi();

app.Run();

public partial class Program;