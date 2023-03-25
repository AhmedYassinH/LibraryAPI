using LibraryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

{

builder.Services.AddControllers();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
}


var app = builder.Build();
{
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

}