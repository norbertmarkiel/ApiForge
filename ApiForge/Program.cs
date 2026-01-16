using ApiForge.Contract;
using ApiForge.Models;
using ApiForge.Runtime;
using Microsoft.AspNetCore.Cors;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

builder.Services.AddSingleton<EndpointRegistry>();
builder.Services.AddSingleton<SchemaValidator>();

var corsPolicyName = "FrontendDev";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, policy =>
    {
        policy
             .WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}


app.UseCors(corsPolicyName);
app.UseAuthorization();

app.MapControllers();

app.MapPost("/admin/endpoints", (
    EndpointDefinition definition,
    EndpointRegistry registry) =>
{
    registry.Register(definition);

    return Results.Created(
        $"/{definition.Path.TrimStart('/')}",
        definition);
});

app.MapPost("/{**path}", async (
    HttpContext context,
    EndpointRegistry registry,
    SchemaValidator validator) =>
{
    var path = "/" + context.Request.Path.Value?.TrimStart('/');

    var endpoint = registry.GetByPath(path);

    if (endpoint is null)
        return Results.NotFound("Endpoint not found");

    if (endpoint.Status != EndpointStatus.Enabled)
        return Results.StatusCode(403);

    using var doc = await JsonDocument.ParseAsync(context.Request.Body);

    var validation = validator.Validate(doc.RootElement, endpoint.Schema);

    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);

    return Results.Ok(new
    {
        message = "Request accepted",
        endpoint = endpoint.Name
    });
});


app.MapGet("/admin/endpoints", (EndpointRegistry registry) =>
{
    return Results.Ok(registry.GetAll());
});

app.MapGet("/admin/endpoints/{id:guid}", (
    Guid id,
    EndpointRegistry registry) =>
{
    var endpoint = registry.GetById(id);
    return endpoint is null
        ? Results.NotFound()
        : Results.Ok(endpoint);
});


app.MapPut("/admin/endpoints/{id:guid}", (
    Guid id,
    EndpointDefinition updated,
    EndpointRegistry registry) =>
{
    if (id != updated.Id)
        return Results.BadRequest("Id cannot be changed");

    var success = registry.Update(id, updated);
    return success ? Results.Ok(updated) : Results.NotFound();
});

app.MapDelete("/admin/endpoints/{id:guid}", (
    Guid id,
    EndpointRegistry registry) =>
{
    var removed = registry.Remove(id);
    return removed ? Results.NoContent() : Results.NotFound();
});
app.Run();
