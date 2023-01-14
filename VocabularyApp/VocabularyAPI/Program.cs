using Microsoft.EntityFrameworkCore;
using VocabularyAPI.Data;
using VocabularyAPI.Data.DBAccesses.VocabularyAccess;
using VocabularyAPI.Extenstions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Connection string is settled in secrets.json file
builder.Services.AddDbContext<VocabularyDBContext>(option =>
    option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IVocabularyActionsAccess, VocabularyActionsAccess>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Creating/deleting and seeding the database
await app.SetupDatabaseAsync(
    builder.Configuration["StaticFilesPaths:SeedData:Path"]);

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
