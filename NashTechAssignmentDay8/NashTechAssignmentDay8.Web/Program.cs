
using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay8.Infrastucture.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("NashTechAssignmentDay8.Web"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();


app.Run();

