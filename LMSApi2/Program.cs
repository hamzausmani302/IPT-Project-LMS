using LMSApi2.Authorization;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LMSApi2.Helpers;
using LMSApi2.Services.Teachers;
using LMSApi2.Services.Users;
using System.Text.Json.Serialization;
using LMSApi2.Services.ClassServices;
using LMSApi2.Services.FileUploadService;
using LMSApi2.Services.CourseService;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

////////////////////creating directories for app  /// remove this when data goes to azure

string rootPath = Directory.GetCurrentDirectory();
string savePath = rootPath + @"/" + builder.Configuration.GetSection("AppSettings")["saveFolderPath"];
if (!Directory.Exists(Path.GetFullPath(builder.Configuration.GetSection("AppSettings")["saveFolderPath"])))
{

    Directory.CreateDirectory(savePath);
}
if (!Directory.Exists(Path.Combine(Path.GetFullPath(builder.Configuration.GetSection("AppSettings")["saveFolderPath"]) , "submissions"))) 
{

    Directory.CreateDirectory(Path.Combine(savePath , "submissions"));
}

//////////////////////////////////


builder.Services.AddDbContext<DataContext>();
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(opt => {
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInstructorService , InstructorService>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IClassService , ClassService>();
builder.Services.AddScoped <ICourseService , CourseService> ();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
  app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/user"), appbuilder =>
{
    appbuilder.UseMiddleware<LMSApi2.Authorization.AuthorizationUser.JWTMiddleware>();
});


app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/teacher"), appbuilder =>
{
    appbuilder.UseMiddleware<LMSApi2.Authorization.AuthorizationTeacher.JWTMiddleWare>();
});

app.MapControllers();

app.Run();
