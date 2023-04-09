using API.Database;
using API.Services.AuthenticationServices;
using API.Services.NoteCategoryServices;
using API.Services.NoteServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// SERVICES
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<INoteCategoryService, NoteCategoryService>();
builder.Services.AddSingleton<INoteService, NoteService>();
// --- Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( options => 
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor= true,
        ValidateAudience= true,
        ValidateLifetime= true,
        ValidateIssuerSigningKey= true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});
builder.Services.AddAuthorization(options => {
    options.AddPolicy("LoggedInUserPolicy", policy => policy.RequireClaim("LoggedInUser Role"));
});
// --- Cors
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
//--- Database
builder.Services.AddDbContext<DatabaseDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("defaultConnection")));
// --- Controllers
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
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
