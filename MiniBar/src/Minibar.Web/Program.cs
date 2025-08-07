using Minibar.Web;
using Minibar.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

app.UseExceptionMiddleware(); // свой extension метод

app.Services.AddBuildDependencies();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Minibar"));
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
