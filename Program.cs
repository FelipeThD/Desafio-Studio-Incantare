using BackendTraining.Repositories;
using BackendTraining.Repositories.Interfaces;
using BackendTraining.Services;
using BackendTraining.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Npgsql;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("PgConnection");
    return new NpgsqlConnection(connectionString);
});

builder.Services.AddScoped<IContactsRepository, ContactsRepository>();
builder.Services.AddScoped<ContactsService>();

builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ContactsValidator>();


builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


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
