using Microsoft.EntityFrameworkCore;
using ACG_Api.Database;
using ACG_Api.service;
using ACG_Api.model.XPath;
using ACG_Api.service.AutoDocService;
using Microsoft.Extensions.Options;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//EF CORE CONNECTON

builder.Services.AddDbContext<DatabaseModel>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(false);
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder
        .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
        .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
});

// builder.Services.AddDbContext<NewDatabaseModel>(options =>
// {
//     options.UseSqlite(builder.Configuration.GetConnectionString("NewDatabaseConnection"));
//     options.EnableSensitiveDataLogging(false);
//     options.UseLoggerFactory(LoggerFactory.Create(builder => builder
//         .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
//         .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
// });

// builder.Services.AddDbContext<DataBaseArchive>(options =>
// {
//     options.UseSqlite(builder.Configuration.GetConnectionString("ArchiveConnection"));
//     options.UseLoggerFactory(LoggerFactory.Create(builder => builder
//         .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
//         .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
// });

// builder.Services.AddDbContext<DataBaseUser>(options =>
// {
//     options.UseSqlite(builder.Configuration.GetConnectionString("UserConnection"));
//     options.UseLoggerFactory(LoggerFactory.Create(builder => builder
//         .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
//         .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
// });

// builder.Services.AddDbContext<MemoryDb>(options =>
// {
//     options.UseInMemoryDatabase("InMemoryDb");
//     options.UseLoggerFactory(LoggerFactory.Create(builder => builder
//         .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
//         .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
// });

//TASKMANAGER CONECCTION TO DATABASE
//builder.Services.AddDbContext<TaskManager>(options =>
//{
//    options.UseSqlite(builder.Configuration.GetConnectionString("TaskManagerConnection"));
//    options.EnableSensitiveDataLogging(false);
//    options.UseLoggerFactory(LoggerFactory.Create(builder => builder
//        .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning)
//        .AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning)));
//});

//PATH TO FOLDER FOR SAVE AGREEMENTS
builder.Services.Configure<PathSettings>(
    builder.Configuration.GetSection("PathToSaveAgreements"));

//Denpendency Injections
builder.Services.AddScoped<GuardService>();
builder.Services.AddTransient<Func<string, XPath>>(provider =>
{
    var options = provider.GetRequiredService<IOptions<PathSettings>>();
    return path => new XPath(path);
});
builder.Services.AddTransient<SubleseTovDog>();
builder.Services.AddTransient<SubleaseTovTermination>();
builder.Services.AddTransient<SubleaseFopDogAct>();
builder.Services.AddTransient<SubleaseFopReturnAct>();
builder.Services.AddTransient<SubleaseFopTermination>();

//CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: MyAllowSpecificOrigins,
//                      policy =>
//                      {
//                          policy.WithOrigins("http://localhost:3000", "http://localhost:5175")
//                                .AllowAnyMethod()
//                                .AllowAnyHeader();
//                      });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173","http://localhost:3000" )
                                .AllowAnyHeader()  // Разрешить любые заголовки
                                .AllowAnyMethod()  // Разрешить любые методы (GET, POST, PUT, DELETE)
                                .AllowCredentials(); // При необходимости можно разрешить отправку учетных данных (например, куки)
                      });
});

// CERTIFICATE HTTPS & HTTP SETTINGS
// var certificatePath = builder.Configuration["CertificatePath"];
// var certificatePassword = builder.Configuration["CertificatePassword"];
// var cert = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);

// var httpsIP = string.IsNullOrWhiteSpace(builder.Configuration["HttpsIP"])
//     ? IPAddress.Parse("0.0.0.0")
//     : IPAddress.Parse(builder.Configuration["HttpsIP"]);

// var staticIP = string.IsNullOrWhiteSpace(builder.Configuration["StaticIP"])
//     ? IPAddress.Parse("0.0.0.0")
//     : IPAddress.Parse(builder.Configuration["StaticIP"]);

// var staticPortHttps = int.Parse(builder.Configuration["StaticPortHttps"] ?? "5001");
// var staticPortHttp = int.Parse(builder.Configuration["StaticPortHttp"] ?? "8080");

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.Listen(httpsIP, staticPortHttps, listenOptions =>
//     {
//         listenOptions.UseHttps(cert);
//     });

//     options.Listen(staticIP, staticPortHttp);
// });

string staticFilesPath = builder.Configuration["StaticFilesPath"] ?? "u'r file path";

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//PATH TO PDF FILES
app.UseStaticFiles();

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(staticFilesPath),
//     RequestPath = "/pdf",
// });

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
