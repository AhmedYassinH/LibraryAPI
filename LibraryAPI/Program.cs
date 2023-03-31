using System.Reflection;
using LibraryAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

{

builder.Services.AddControllers();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Library Managment System API",
        Description = "An ASP.NET Core Web API for managing a Library system",
        Contact = new OpenApiContact
        {
            Name = "Ahmed Yassin",
            Email= "ahmed.y.hamid@gmail.com",
            Url = new Uri("https://github.com/AhmedYassinH/LibraryAPI")
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
}


var app = builder.Build();
{

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


}