using NeoVortex.Application.Common.DepedencyInjection;
using NeoVortex.Infrastructure.Common.DependencyInjection;
using NeoVortex.Presentation.Common.DependencyInjection;
using NeoVortex.Presentation.Common.HttpConfigurations;
using NeoVortex.Presentation.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCustomProblemDetails();
builder.Services.AddUtilities();

builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddControllers();

var app = builder.Build();

await app.UseTriggerSeeder();

app.UseCors(config => { config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
app.MapOpenApi();
app.UseSwaggerUI(config => { config.SwaggerEndpoint("/openapi/v1.json", "NeoVortex API"); });

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PermissionAuthorizationMiddleware>();

app.MapControllers();

app.Run();