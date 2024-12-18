using Application.Layer;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Infrastructure.Layer.Repositories;
using LibraryWebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//conexi�n SQL
builder.Services.AddDbContext<LibraryDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conectionSQL")));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<LibraryDBContext>().AddDefaultTokenProviders();

//dependencias
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();



var key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");

builder.Services.AddHttpContextAccessor();
//autenticacion
builder.Services.AddAuthentication(
     auth =>
     {
         auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

     } 
    ).AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true

        };
        
    });

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "Autenticaci�n JWT usando el esquema Bearer. \r\n\r\n " +
            "Ingresa la palabra 'Bearer' seguido de un [espacio] y despu�s su token en el campo de abajo.\r\n\r\n" +
            "Ejemplo: \"Bearer tkljk125jhhk\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
});

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

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("policyCors");
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
