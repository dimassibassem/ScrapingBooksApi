using API.Job;
using API.Models.BLL;
using API.Models.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   var res = BLL_BiruniDatbase.GetBiruniDatabase();
   // Console.WriteLine(res);

    // var todo = new CronJob();
    // runs every month on the 1st day of the month at 00:00
    // await todo.Task1();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();