using Application.Layer;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.Repositories;
using LibraryWebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//conexión SQL
builder.Services.AddDbContext<LibraryDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conectionSQL")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<LibraryDBContext>().AddDefaultTokenProviders();

//dependencias
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(p => p.AddPolicy("policyCors", build =>
 build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("policyCors");
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
//app.UseAuthentication();

app.MapControllers();

app.Run();
