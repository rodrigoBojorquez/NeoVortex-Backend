using Microsoft.Extensions.FileProviders;
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

app.UseCors(config => { config.WithOrigins("http://localhost:5173", "http://127.0.0.1:9980").AllowAnyMethod().AllowAnyHeader().AllowCredentials(); });
app.MapOpenApi();
app.UseSwaggerUI(config => { config.SwaggerEndpoint("/openapi/v1.json", "NeoVortex API"); });

var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images");
if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/images"
});

app.UseHttpsRedirection();
app.UseGlobalExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PermissionAuthorizationMiddleware>();

app.MapControllers();

app.Run();