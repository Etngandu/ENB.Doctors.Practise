using ENB.Blazor.Lawyer.HttpRepository;
using ENB.Doctors.Practise.Blazor;
using ENB.Doctors.Practise.Blazor.HttpRepository;
using ENB.Doctors.Practise.EF.Repositories;
using ENB.Doctors.Practise.Entities.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7216/api/") });
builder.Services.AddScoped<IPatientHttpRepository, PatientHttpRepository>();
builder.Services.AddScoped<IStaffHttpRepository, StaffHttpRepository>();
builder.Services.AddScoped<NotificationService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
