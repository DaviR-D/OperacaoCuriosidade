using Api.Modules.Authentication;
using Api.Modules.Registrations;

var builder = WebApplication.CreateBuilder(args);

var registrationsMock = new List<RegistrationDto>();
var usersMock = new List<User>();
usersMock.Add(new User("Davi", "davi@gmail.com", "senha123"));

builder.Services.AddTransient<AuthenticationService>();

builder.Services.AddSingleton(registrationsMock);
builder.Services.AddSingleton(usersMock);

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("policy");

app.UseHttpsRedirection();

app.Run();
