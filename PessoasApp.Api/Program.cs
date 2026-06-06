using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options => {
    options.AddPolicy("BlazorApp", policy => {
        policy.WithOrigins(allowedOrigins!)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.MapScalarApiReference(s => s.WithTheme(ScalarTheme.DeepSpace));

app.UseCors("BlazorApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
