using Microsoft.EntityFrameworkCore;
using CodebaseAPI.Data; 
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder => builder
//            .WithOrigins("https://localhost:7080/api/Customer/check-customer") 
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});


builder.Services.AddControllers();





// Configure Entity Framework and database context
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Codebase API", Version = "v1" });
});




//builder.Services.AddCors(options => options.AddPolicy("BlazorYTPolicy", policyBuilder =>
//{
//    policyBuilder.WithOrigins("https://localhost:7080");
//    policyBuilder.AllowAnyHeader();
//    policyBuilder.AllowAnyMethod();
//}));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Codebase API V1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
