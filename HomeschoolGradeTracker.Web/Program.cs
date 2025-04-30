using Microsoft.EntityFrameworkCore;
using HomeschoolGradeTracker.Infrastructure.Persistence;
using HomeschoolGradeTracker.Application.Interfaces;
using HomeschoolGradeTracker.Infrastructure.Repositories;
using HomeschoolGradeTracker.Application.Subjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Registering the repository and service in the DI container.
// The service and repository are both scoped to the request lifecycle.
// This allows the web layer to use SubjectService, while SubjectService uses the interface,
// and EF Core only lives in the Infrastructure layer.
builder.Services.AddScoped<IRepository, SubjectRepository>();
builder.Services.AddScoped<SubjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
