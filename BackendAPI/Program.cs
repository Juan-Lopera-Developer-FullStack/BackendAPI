using BackendAPI.Models.Configuration;
using BackendAPI.Models.Repository;
using BackendAPI.Models.Repository.IRepository;
using BackendAPI.Services;
using BackendAPI.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ConfigConnection>(builder.Configuration.GetSection("ConfiguracionConexion"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFamilyGroupRepository, FamilyGroupRepository>();
builder.Services.AddScoped<ILogPetitionRepository, LogPetitionRepository>();
builder.Services.AddScoped<IBearerToken, BearerToken>();
builder.Services.AddHttpClient<IJsonPlaceHolder, JsonPlaceHolder>();
builder.Services.AddScoped<IJsonPlaceHolderPostRepository, JsonPlaceHolderPostRepository>();
builder.Services.AddScoped<IJsonPlaceHolderCommentRepository, JsonPlaceHolderCommentRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
