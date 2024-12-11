using Autofac.Extensions.DependencyInjection;
using Autofac;
using CleanCodeAPI.Attributes;
using CleanCodeAPI.Exceptions;
using CleanCodeAPI.Services;
using CleanCodeAPI.Aspects;
using Autofac.Extras.DynamicProxy;
using CleanCodeSolution.Domain.Services;
using CleanCodeSolution.Domain.Repositories;
using CleanCodeSolution.Infrastructure.Repositories;
using System.Reflection;
using CleanCodeSolution.Application.Handlers;
using CleanCodeSolution.Domain.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// attribute nesnesi uygulama register edilip action �zerinde tan�mlan�rsa action onbefore ve onafter s�re�lerini y�netmi� oluyor.
builder.Services.AddTransient<RequestInterceptionAttribute>();
//builder.Services.AddScoped<TestService>();

// Bir Interface birden fazla s�n�ftan t�retildi�i takdirde i�erdek bu davran�� de�i�ikli�ini uygulamak i�in keyedScoped servislerden yararlan�r�z.
builder.Services.AddKeyedScoped<IAccountService, IndividualAccountService>("bireysel");
builder.Services.AddKeyedScoped<IAccountService, CorporateAccountService>("kurumsal");

builder.Services.AddScoped<IAccountRepo,AccountRepo>();

// CloseAccountRequestHandler bu tipte olan assemby projeyi reflection ile load et.
var applicationDll = Assembly.GetAssembly(typeof(CloseAccountRequestHandler));
var domainDll = Assembly.GetAssembly(typeof(AccountClosedHandler));

ArgumentNullException.ThrowIfNull(applicationDll);
ArgumentNullException.ThrowIfNull(domainDll);

// Mediator Handlerslar�n �al��mas� i�in IoC �zerine register edilmesi laz�m
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(applicationDll, domainDll);
});

// Autofac Aspect Registeration AutoFac Module 

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
  builder.RegisterType<BenchmarkAspect>();
  builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
  // TestService ile birlikte BenchmarkAspect attribute kullanmas�n� AutoFac IOC Container register ediyoruz.
  builder.RegisterType<TestService>().As<ITestService>().AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(BenchmarkAspect));

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); // Aspect Oriented Programing Kod blo�unun aras�na girip t�m uygulamadaki kod bloklar�n�n try catch ile sarmalad�k. Hatalar� merkezile�tirdik.

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
