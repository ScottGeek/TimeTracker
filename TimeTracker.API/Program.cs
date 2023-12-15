
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme { 
     In = ParameterLocation.Header,
     Name = "Authorization",
     Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

ConfigureMapster();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

//.AddSignInManager<SignInManager<User>>()

//builder.Services.AddIdentityCore<User>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 4;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.User.RequireUniqueEmail = true;


//})
//    .AddEntityFrameworkStores<DataContext>()
//    .AddSignInManager<SignInManager<User>>()
//    .AddDefaultTokenProviders();

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
             ValidIssuer = builder.Configuration["JwtIssuer"],
             ValidAudience = builder.Configuration["JwtAudience"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]!)),
         };
     });

 //.AddIdentityCookies();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();

void ConfigureMapster()
{
    TypeAdapterConfig<Project, ProjectResponse>.NewConfig()
        .Map(dest => dest.Description, src => src.ProjectDetails != null ? src.ProjectDetails.Description : null)
        .Map(dest => dest.StartDate, src => src.ProjectDetails != null ? src.ProjectDetails.StartDate : null)
        .Map(dest => dest.EndDate, src => src.ProjectDetails != null ? src.ProjectDetails.EndDate : null);


}
