using backend.Repositories;
using backend.Services;
using backend.Data;
using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using backend.Repositories.IRepositories;
using backend.Services.IServices;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddControllers();

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IExpenseServices, ExpenseServices>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IRepository<GroupInviteModel>, Repository<GroupInviteModel>>();
builder.Services.AddScoped<IGroupInviteService, GroupInviteService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBconnection"))
    );
builder.Services.AddScoped(typeof(Repository<>));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddScoped<AuthService>();

builder.Services.Configure<JwtToken>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton(
    resolver => resolver.GetRequiredService<IOptions<JwtToken>>().Value
    );

// Middleware settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtToken>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// For Swagger test (Optional)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Inserisci il token JWT con la parola 'Bearer' davanti (es. 'Bearer abc123')",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
