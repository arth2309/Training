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
builder.Services.AddScoped<IViewNoteServices, ViewNoteServices>();
builder.Services.AddScoped<IRequestNoteRepo, RequestNoteRepo>();
builder.Services.AddScoped<IForgotPasswordServices, ForgotPasswordServices>();
builder.Services.AddScoped<ICancelCaseServices, CancelCaseServices>();
builder.Services.AddScoped<IRequestStatusLogRepo, RequestStatusLogRepo>();
builder.Services.AddScoped<IRegionRepo, RegionRepo>();
builder.Services.AddScoped<IAssignCaseServices, AssignCaseServices>();
builder.Services.AddScoped<IPhysicianRepo, PhysicianRepo>();
builder.Services.AddScoped<IBlockCaseServices, BlockCaseServices>();
builder.Services.AddScoped<IBlockedRequestRepo, BlockedRequestRepo>();
builder.Services.AddScoped<IViewUploadsServices, ViewUploadsServices>();
builder.Services.AddScoped<IJwtServices, JwtServices>();
builder.Services.AddScoped<IAspNetUserRepo, AspNetUserRepo>();
builder.Services.AddScoped<ISendOrderServices, SendOrderServices>();
builder.Services.AddScoped<IProfessionRepo, ProfessionRepo>();
builder.Services.AddScoped<IVendorRepo, VendorRepo>();
builder.Services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
builder.Services.AddScoped<IClearCaseServices, ClearCaseServices>();
builder.Services.AddScoped<ISendAgreementServices,SendAgreementServices>();
builder.Services.AddScoped<IPasswordHashServices, PasswordHashServices>();
builder.Services.AddScoped<IPatientUserProfileServices, PatientUserProfileServices>();
builder.Services.AddScoped<IPatientShowDocumentsServices, PatientShowDocumentsServices>();
builder.Services.AddScoped<IRequestBusinessRepo, RequestBusinessRepo>();
builder.Services.AddScoped<IRequestConceirgeRepo, RequestConceirgeRepo>();
builder.Services.AddScoped<IPatientSendRequestServices, PatientSendRequestServices>();
builder.Services.AddScoped<ICloseCaseServices, CloseCaseServices>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();
builder.Services.AddScoped<IAdminProfileServices, AdminProfileServices>();
builder.Services.AddScoped<IAdminProviderInfoServices, AdminProviderInfoServices>();
builder.Services.AddScoped<IMenuRepo, MenuRepo>();
builder.Services.AddScoped<IAdminAccessRoleServices, AdminAccessRoleServices>();
builder.Services.AddScoped<IEncounterFormServices, EncounterFormServices>();
builder.Services.AddScoped<IEncounterRepo, EncounterRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();







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
