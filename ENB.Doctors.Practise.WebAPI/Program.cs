using AutoMapper;
using ENB.Doctors.Practise.EF;
using ENB.Doctors.Practise.EF.Repositories;
using ENB.Doctors.Practise.Entities;
using ENB.Doctors.Practise.Entities.Repositories;
using ENB.Doctors.Practise.Infrastructure;
using ENB.Doctors.Practise.WebAPI.Help;
using ENB.Doctors.Practise.WebAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DoctorPractiseContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("DoctorPractiseCstrg")));
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
//               opt =>
//               {
//                   opt.Password.RequiredLength = 7;
//                   opt.Password.RequireDigit = false;
//                   opt.Password.RequireUppercase = false;
//               })
//                .AddEntityFrameworkStores<DoctorPractiseContext>();

//builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsFactory>();
builder.Services.AddAutoMapper(typeof(DoctorPractiseProfile));
builder.Services.AddScoped<IMapper, Mapper>();
builder.Services.AddScoped<IAsyncPatientRepository, AsyncPatientRepository>();
builder.Services.AddScoped<IAsyncStaffRepository, AsyncStaffRepository>();
builder.Services.AddScoped<IAsyncUnitOfWorkFactory, AsyncEFUnitOfWorkFactory>();
//builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddScoped<IValidator<CreateAndEditPatient>, CreateAndEditPatientValidator>();


builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ENB-Doctors-Practise-API",
        Version = "v1",
        Description = "ENB-Doctors-Practise-API",
        Contact = new OpenApiContact
        {
            Name = "Etienne Ngandu Bukasa",
            Email = "etngandu@hotmail.com",
            Url = new Uri("https://etngandu.be"),
        },
        License = new OpenApiLicense
        {
            Name = "ENB OpenLicence",
            Url = new Uri("https://etngandu.be"),
        }

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
