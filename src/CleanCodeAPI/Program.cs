using Autofac.Extensions.DependencyInjection;
using Autofac;
using CleanCodeAPI.Attributes;
using CleanCodeAPI.Exceptions;
using CleanCodeAPI.Services;
using CleanCodeAPI.Aspects;
using Autofac.Extras.DynamicProxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// attribute nesnesi uygulama register edilip action üzerinde tanımlanırsa action onbefore ve onafter süreçlerini yönetmiş oluyor.
builder.Services.AddTransient<RequestInterceptionAttribute>();
//builder.Services.AddScoped<TestService>();


// Autofac Aspect Registeration AutoFac Module 

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
  builder.RegisterType<BenchmarkAspect>();
  builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
  // TestService ile birlikte BenchmarkAspect attribute kullanmasını AutoFac IOC Container register ediyoruz.
  builder.RegisterType<TestService>().As<ITestService>().AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(BenchmarkAspect));

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>(); // Aspect Oriented Programing Kod bloğunun arasına girip tüm uygulamadaki kod bloklarının try catch ile sarmaladık. Hataları merkezileştirdik.

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
