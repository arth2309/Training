using ProjectManagementSystemRepo.Interface;
using ProjectManagementSystemServices.Implementation;
using ProjectManagementSystemServices.Interface;
using ProjectManagementSystemRepo.Implementation;
using ProjectManagementSystemRepo.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IDashBoardServices,DashBoardServices>();
builder.Services.AddScoped<IProjectRepo, ProjectRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DashBoard}/{action=ProjectDashBoard}/{id?}");

app.Run();
