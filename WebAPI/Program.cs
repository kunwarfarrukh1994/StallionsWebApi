using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using WEBAPI.DAL;




var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApp(options =>
//    {
//        builder.Configuration.Bind("AzureAdB2C", options);
//        options.Events ??= new OpenIdConnectEvents();
//        options.SaveTokens = true;

//    });


////builder.Services.AddAuthentication()
////    .AddJwtBearer("AzureAD", options =>
////    {
////        Configuration.Bind("AzureAdB2C", options);
////        options.Authority = $"{Configuration["AzureAdB2C:Instance"]}{Configuration["AzureAdB2C:Domain"]}/{Configuration["AzureAdB2C:SignUpSignInPolicyId"]}/v2.0/";
////        options.Audience = Configuration["AzureAdB2C:ClientId"];

////    });


// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer("azuread", jwtOptions =>
  {
      jwtOptions.Authority = $"https://stallionsdomian.b2clogin.com/{builder.Configuration.GetSection("AzureAd:TenantId")}/{builder.Configuration.GetSection("AzureAd:policy")}/v2.0/";
      //jwtOptions.Authority = $"https://login.microsoftonline.com/eb8cf9f7-9cb6-4112-9ba6-ab8adfec1e04/v2.0/";
      //jwtOptions.Audience = builder.Configuration.GetSection("AzureAd:ClientId").ToString();
      jwtOptions.Audience = "6af3fd85-3b74-4df8-bbb6-bb8da7240ebf";
      jwtOptions.Events = new JwtBearerEvents
      {
          //OnAuthenticationFailed = AuthenticationFailed
      };
  });
//builder.Services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme).AddAzureADBearer(option => builder.Configuration.Bind("AzureAd", option));

builder.Services.AddControllers();

builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();
//app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");
app.Run();
