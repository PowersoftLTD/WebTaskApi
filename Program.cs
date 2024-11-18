using TaskManagement.API.DapperDbConnections;
using TaskManagement.API.Interfaces;
using TaskManagement.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Configure database connection using Entity Framework Core
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthonticatServerConnectionString")));

// Configure CORS to allow all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()    // Allow any origin
               .AllowAnyMethod()   // Allow any HTTP method
               .AllowAnyHeader();  // Allow any header
    });
});

builder.Services.AddScoped<IJWTConfigure, JWTConfigure>();
builder.Services.AddScoped<ITASKRepository, TASKRepository>();
builder.Services.AddScoped<IDoc_Temp, DocumentTemplateRepository>();
builder.Services.AddScoped<IDapperDbConnection, DapperDbConnection>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IEmployeeMst, EmployeeMasterRepository>();
builder.Services.AddScoped<IViewClassification, VIewClassification>();
builder.Services.AddScoped<IApprovalTemplate, ApprovalTemplateRepository>();
builder.Services.AddScoped<IProjectDefination, ProjectDefinationRespository>();
// Register IConfiguration to access connection strings
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register connection string explicitly
builder.Services.AddScoped(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return configuration.GetConnectionString("AuthonticatServerConnectionString");
});


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var keyProvider = builder.Services.BuildServiceProvider().GetRequiredService<IJWTConfigure>();
var jwtKey = await keyProvider.JWTToken();
var JWTKEY = jwtKey.FirstOrDefault();
var key = Encoding.UTF8.GetBytes(JWTKEY.Key);
if (key.Length < 32)
{
    throw new InvalidOperationException("The key must be at least 32 bytes long.");
}
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
        ValidIssuer = JWTKEY.Issuer,
        ValidAudience = JWTKEY.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKEY.Key)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "TM API", Version = "v1" });
   // c.OperationFilter<OpenApiParameterIgnoreFilter>();

    // Add security definitions for JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' followed by a space and then your token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new string[] {} }
    });
});



builder.Services.AddAuthorization();
//builder.Services.AddDbContext<TaskManagementAuthDbContext>();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
// Use CORS
app.UseCors("AllowAllOrigins");
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.MapControllers();

app.Run();
