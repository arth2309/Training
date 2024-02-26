using HalloDoc.DataContext;
using HalloDoc.Repositories.Interfaces;
using HalloDoc.Repositories.Implementation;
using HallodocServices.Interfaces;
using HallodocServices.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HalloDoc.DataContext.ApplicationDbContext>();
builder.Services.AddScoped<IPatientLoginRepo, PatientLoginRepo>();
builder.Services.AddDbContext<HalloDoc.Repositories.DataContext.ApplicationDbContext>();
builder.Services.AddScoped<IPatientLoginServices, PatientLoginServices>();
builder.Services.AddScoped<IPatientDashBoardServices, PatientDashBoardServices>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRequestRepo, RequestRepo>();
builder.Services.AddScoped<IRequestFileRepo, RequestFileRepo>();
builder.Services.AddScoped<IAdminDashBoardServices, AdminDashBoardServices>();
builder.Services.AddScoped<IRequestClientRepo, RequestClientRepo>();
builder.Services.AddScoped<IViewCaseServices, ViewCaseServices>();




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
    pattern: "{controller=Patient}/{action=Index}/{id?}");

app.Run();
