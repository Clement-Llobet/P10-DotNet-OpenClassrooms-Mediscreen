using Mediscreen.UI.Controllers.Services.PatientServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://api.example.com/"); // Replace with your API base URL
});

builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
