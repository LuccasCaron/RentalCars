using FluentValidation.AspNetCore;
using RentalCars.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
                });

builder.Services.AddEndpointsApiExplorer();

var configuration = builder.Configuration;

builder.Services.AddInfraestructure(configuration);

var app = builder.Build();

app.RegisterMigrations();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


