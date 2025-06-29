using Microsoft.EntityFrameworkCore;
using Minibar.Web;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Minibar"));
}

// app.UseHttpsRedirection();

// app.UseAuthorization();
app.MapControllers();

app.Run();
