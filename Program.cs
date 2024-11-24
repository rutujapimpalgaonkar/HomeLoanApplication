// using HomeLoanApplication.Data;
// using HomeLoanApplication.Repositories;
// using HomeLoanApplication.Services;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Authentication.JwtBearer;  // Add this for JWT authentication
// using Microsoft.IdentityModel.Tokens;                // Add this for TokenValidation
// using System.Text;


// var builder = WebApplication.CreateBuilder(args);

// // Add CORS policy
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAllOrigins", builder =>
//     {
//         builder.AllowAnyOrigin()          // Allow all origins (for development; for production, restrict this)
//                .AllowAnyMethod()          // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)
//                .AllowAnyHeader();         // Allow any headers
//     });
// });

// // JWT Authentication Configuration
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.RequireHttpsMetadata = false;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//             ValidAudience = builder.Configuration["JwtSettings:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
//         };
//     });


// // Add services to the container
// builder.Services.AddDbContext<HomeLoanContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ensure connection string is correct

// // Registering repositories and services for dependency injection
// builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
// builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();

// builder.Services.AddScoped<IAccountRepository, AccountRepository>();
// builder.Services.AddScoped<IAccountService, AccountService>();

// builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
// builder.Services.AddScoped<IDocumentService, DocumentService>();

// builder.Services.AddScoped<ILoanTrackerRepository, LoanTrackerRepository>();
// builder.Services.AddScoped<ILoanTrackerService, LoanTrackerService>();

// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUserService, UserService>();




// // Adding the Email Notification service 
// builder.Services.AddScoped<IEmailService, EmailService>();

// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // Adding the AuthService 
// builder.Services.AddScoped<IAuthService, AuthService>();
// builder.Services.AddScoped<IUserRepository, UserRepository>();


// // Adding the IncomeDetail service and repository
// builder.Services.AddScoped<IIncomeDetailRepository, IncomeDetailRepository>();
// builder.Services.AddScoped<IIncomeDetailService, IncomeDetailService>();

// // Add controllers
// builder.Services.AddControllers();  // Ensure this line is included

// // Add Swagger for API documentation
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();  // This line should be included

// var app = builder.Build();

// // Use HTTPS Redirection
// app.UseHttpsRedirection();

// // Configure the HTTP request pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger(); // Ensure this is enabled
//     app.UseSwaggerUI();  // Enable Swagger UI for API documentation
// }

// app.UseAuthentication();  // Add this line to enable authentication
// app.UseAuthorization();   // Add this line to enable authorization

// app.MapControllers();  // Ensure this line is included to map controller routes

// app.Run();


using HomeLoanApplication.Data;
using HomeLoanApplication.Repositories;
using HomeLoanApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;  // Add this for JWT authentication
using Microsoft.IdentityModel.Tokens;                // Add this for TokenValidation
using Microsoft.OpenApi.Models;                      // For Swagger Configuration
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()          // Allow all origins (for development; for production, restrict this)
               .AllowAnyMethod()          // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)
               .AllowAnyHeader();         // Allow any headers
    });
});

// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

// Add services to the container
builder.Services.AddDbContext<HomeLoanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Ensure connection string is correct

// Registering repositories and services for dependency injection
builder.Services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();

builder.Services.AddScoped<IIncomeDetailRepository, IncomeDetailRepository>();
builder.Services.AddScoped<IIncomeDetailService, IncomeDetailService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddScoped<ILoanTrackerRepository, LoanTrackerRepository>();
builder.Services.AddScoped<ILoanTrackerService, LoanTrackerService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IEmailService, EmailService>();

// Adding the AuthService 
builder.Services.AddScoped<IAuthService, AuthService>();

// Add controllers
builder.Services.AddControllers();  // Ensure this line is included

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Add JWT Bearer token support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    // Add security requirement for all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Use HTTPS Redirection
app.UseHttpsRedirection();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the root of the site (optional)
    });
}

app.UseAuthentication();  // Add this line to enable authentication
app.UseAuthorization();   // Add this line to enable authorization

app.MapControllers();  // Ensure this line is included to map controller routes

app.Run();
