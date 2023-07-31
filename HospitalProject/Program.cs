using HospitalProject.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<SpecialityDataContext>();
builder.Services.AddScoped<PersonDataContext>();
builder.Services.AddScoped<PageDataContext>();
builder.Services.AddScoped<MedicineDataContext>();
builder.Services.AddScoped<CampusDataContext>();
builder.Services.AddScoped<TypeUserDataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
