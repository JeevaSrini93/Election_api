
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Test_APi.model;
using Test_APi.Service;
using Test_APi.ServiceMpSeatService;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
builder.Services.AddMvc();
builder.Services.AddDbContextPool<ElectionManagerContext>(
      options => { options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); options.EnableSensitiveDataLogging(); });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test.Api", Version = "v1" });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecurityKey"]);

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IVoterService, VoterService>();

builder.Services.AddScoped<IMPService, MpSeatService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IVoterService, VoterService>();
builder.Services.AddScoped<IVotingSystemService, VotingSystemService>();
//socped
//transit

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseRouting();

app.MapControllers();

app.Run();
