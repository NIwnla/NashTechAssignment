using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using NashTechAssignmentDay9.Infrastructure.Data;
using NashTechAssignmentDay9.Web.Extensions;
using NashTechAssignmentDay9.Application.Common.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"), b => b.MigrationsAssembly("NashTechAssignmentDay9.Web"));
});

builder.Services.AddRepositories();
builder.Services.AddServices();




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

app.SeedData();

app.Run();

