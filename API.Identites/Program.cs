﻿using APP.Identities;
using APP.Identities.Domain;
using APP.Identities.Features;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
//IOC:
var connectionString = builder.Configuration.GetConnectionString("IdentitiesDb");
builder.Services.AddDbContext<IdentitiesDb>(options => options.UseSqlite(connectionString));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(IdentityDbHandler).Assembly));

//AppSettings
var section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

// ======================================================
// AUTHENTICATION
// ======================================================

// Enable JWT Bearer authentication as the default scheme.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(config =>
    {
        // Define rules for validating JWT tokens.
        config.TokenValidationParameters = new TokenValidationParameters
        {
            // Match the token's issuer to the expected issuer from AppSettings.
            ValidIssuer = AppSettings.Issuer,

            // Match the token's audience to the expected audience.
            ValidAudience = AppSettings.Audience,

            // Use the symmetric key defined in AppSettings to verify the token's signature.
            IssuerSigningKey = AppSettings.SigningKey,

            // These flags ensure thorough validation of the token.
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ======================================================
// SWAGGER
// ======================================================

// Configure Swagger/OpenAPI documentation, including JWT auth support in the UI.
builder.Services.AddSwaggerGen(c =>
{
    // Define the basic information for your API.
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API",
        Version = "v1"
    });

    // Add the JWT Bearer scheme to the Swagger UI so tokens can be tested in requests.
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. "
        + "Enter your token as: Bearer your_token_here " +
        " Example: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
    });

    // Add the security requirement globally so all endpoints are secured unless specified otherwise.
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ======================================================
// AUTHENTICATION
// ======================================================

// Enable authentication middleware so that [Authorize] works.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
