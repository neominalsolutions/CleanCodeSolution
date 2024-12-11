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

// attribute nesnesi uygulama register edilip action üzerinde tanýmlanýrsa action onbefore ve onafter süreçlerini yönetmiþ oluyor.
builder.Services.AddTransient<RequestInterceptionAttribute>();
//builder.Services.AddScoped<TestService>();

// Bir Interface birden fazla sýnýftan türetildiði takdirde içerdek bu davranýþ deðiþikliðini uygulamak için keyedScoped servislerden yararlanýrýz.
builder.Services.AddKeyedScoped<IAccountService, IndividualAccountService>("bireysel");
builder.Services.AddKeyedScoped<IAccountService, CorporateAccountService>("kurumsal");

builder.Services.AddScoped<IAccountRepo,AccountRepo>();

// CloseAccountRequestHandler bu tipte olan assemby projeyi reflection ile load et.
var applicationDll = Assembly.GetAssembly(typeof(CloseAccountRequestHandler));
var domainDll = Assembly.GetAssembly(typeof(AccountClosedHandler));

ArgumentNullException.ThrowIfNull(applicationDll);
ArgumentNullException.ThrowIfNull(domainDll);

// Mediator Handlerslarýn çalýþmasý için IoC üzerine register edilmesi lazým
builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssemblies(applicationDll, domainDll);
});

// Autofac Aspect Registeration AutoFac Module 

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
  builder.RegisterType<BenchmarkAspect>();
  builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
  // TestService ile birlikte BenchmarkAspect attribute kullanmasýný AutoFac IOC Container register ediyoruz.
  builder.RegisterType<TestService>().As<ITestService>().AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(BenchmarkAspect));

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); // Aspect Oriented Programing Kod bloðunun arasýna girip tüm uygulamadaki kod bloklarýnýn try catch ile sarmaladýk. Hatalarý merkezileþtirdik.

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
