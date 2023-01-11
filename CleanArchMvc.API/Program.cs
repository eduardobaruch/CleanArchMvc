using CleanArchMvc.Infra.IoC;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Setting Swagger
builder.Services.AddInfrastructureSwagger(builder.Configuration);


// Setting the JWT token
builder.Services.AddInfrastructureJWT(builder.Configuration);

// Setting the interfaces, automap, mediator, identity
builder.Services.AddInfrastructureAPI(builder.Configuration);

// Ignoring JSON cyclic reference
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Show status of the code. Ex: 401 instead of undocumented, will show unauthorized
app.UseStatusCodePages(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
