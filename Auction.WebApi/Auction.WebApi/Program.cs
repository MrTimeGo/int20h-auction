using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using Auction.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Auction.WebApi.Entities;
using Microsoft.AspNetCore.Identity;
using Auction.WebApi.Options;
using Auction.WebApi.Services.Interfaces;
using Auction.WebApi.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Auction.WebApi.Middlewares;
using Auction.WebApi.Extentions;
using Auction.WebApi.Hubs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AuctionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddIdentityCore<User>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // User settings.
    options.User.AllowedUserNameCharacters = string.Empty;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AuctionContext>()
    .AddSignInManager<SignInManager<User>>();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<S3Options>(
    builder.Configuration.GetSection("AWS"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IUploadFileService, UploadFileService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddSignalR();

//if (builder.Environment.IsDevelopment())
//    builder.Services.AddDevAws(builder.Configuration);
//else
//    builder.Services.AddProdAws(builder.Configuration);

builder.Services.AddDevAws(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionHandlerFilter>();
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<CurrentUserMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var allowedOrigins = (builder.Configuration.GetValue<string>("AllowedOrigins") ?? "").Split(';');

app.UseCors(x => x.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware>();

app.MapHub<ChatHub>("/api/chatHub");
app.MapHub<BetHub>("/api/betHub");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<AuctionContext>()
        .Database.MigrateAsync();
}

app.Run();
