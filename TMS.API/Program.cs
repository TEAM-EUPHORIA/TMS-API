<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
using Serilog;
using TMS.API;
using TMS.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// adding serilog
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

// builder.Logging.ClearProviders(); //if its enabled console loggings won't work
builder.Logging.AddSerilog(logger);

// Making Db Context available for the App 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Making UserService available for the App
builder.Services.AddTransient<UserService>();

<<<<<<< HEAD
=======
builder.Services.AddTransient<CourseFeedbackService>();
builder.Services.AddTransient<TraineeFeedbackService>();


builder.Services.AddTransient<ReviewService>();
builder.Services.AddTransient<CourseService>();

builder.Services.AddTransient<DepartmentService>();
builder.Services.AddTransient<DashboardService>();


>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allowing angular to request api
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
<<<<<<< HEAD
    {
=======
    { 
>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

<<<<<<< HEAD
=======

>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
<<<<<<< HEAD
app.UseCors("default");
=======

app.UseCors("default");

>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

<<<<<<< HEAD
app.Run();
=======
app.Run();
>>>>>>> 9f4ee4676bf74e4a6594aa9216c0f03956d03779
