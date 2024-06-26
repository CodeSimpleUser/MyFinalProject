using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstact;
using DataAccess.Concrete.EntitiyFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject --> IoC Container
// AOP
builder.Services.AddControllers();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var tokenoptions = Configuration.GetSection("TokenOptions").GetSection<TokenOptions>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenoptions.Issuer,
        ValidAudience = tokenoptions.Audience,
        ValidateIssuerSigningKey = true,
       IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenoptions.Security)
    };
    
});
builder.Services.

//builder.Host.ConfigureContainer<ContainerBuilder>(container =>
//{
//    container.RegisterModule(new AutofacBusinessModule());
//});

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

app.MapControllers();

app.Run();
