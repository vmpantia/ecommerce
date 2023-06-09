using ECommerce.BAL.Contractors;
using ECommerce.BAL.Services;
using ECommerce.BAL.Utilities;
using ECommerce.DAL.Contractors;
using ECommerce.DAL.DataAccess;
using ECommerce.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ECommerceDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("QA"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IEmailUtil, EmailUtil>();
builder.Services.AddScoped<IFileUtil, FileUtil>();
builder.Services.AddScoped<IJwtUtil, JwtUtil>();
builder.Services.AddScoped<IValidateUtil, ValidateUtil>();
builder.Services.AddScoped<IPasswordUtil, PasswordUtil>();


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000/")
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

// Setup JWT Token for Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("Policy"); /*Use Policy CORS*/
app.UseAuthentication(); /*Use JWT Authentication*/
app.UseAuthorization();

app.MapControllers();

app.Run();
