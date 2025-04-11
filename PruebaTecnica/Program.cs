using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PruebaTecnica.Aplication.VirtualMachines.V1;
using PruebaTecnica.Application.Exceptions;
using PruebaTecnica.Application.Exceptions.Interfaces;
using PruebaTecnica.Application.Hubs;
using PruebaTecnica.Application.Interfaces;
using PruebaTecnica.Application.Services;
using PruebaTecnica.Data.Context;
using PruebaTecnica.Data.Repository;
using PruebaTecnica.Data.Seed;
using PruebaTecnica.Domain.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PruebaTecnicaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaTecnicaDbConnection"));
});

// Configurar Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PruebaTecnicaContext>()
    .AddDefaultTokenProviders();

// Configurar autenticación JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//JwSecurity
builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddTransient<IVirtualMachineRepository, VirtualMachineRepository>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddTransient<PruebaTecnicaContext>();
builder.Services.AddExceptionServices(typeof(LoadExceptionAssembly).Assembly);

builder.Services.AddValidationsAndServicesVirtualMachine();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200", "https://cerulean-youtiao-2225da.netlify.app")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        );
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseExceptionHandlerMiddleware(app.Environment, app.Services.GetRequiredService<IExceptionHandlerService>()!);

// Ejecutar migraciones y seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PruebaTecnicaContext>();
    context.Database.Migrate();
    await SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();
app.MapHub<VirtualMachineHub>("/virtualMachineHub");

app.Run();
