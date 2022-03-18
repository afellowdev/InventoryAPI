using InventoryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var myAllowSpecificOrigion = "_myAllowSpecificOrigion";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//kev   add DbContext for InventoryAPI
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConn"));
});

//kev   enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigion,
        builder =>
        {
            //builder.WithOrigins("http://localhost")
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//kev: use CORS
app.UseCors(myAllowSpecificOrigion);

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = "/Photos"
});

app.Run();
