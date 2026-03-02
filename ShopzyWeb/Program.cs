using Microsoft.EntityFrameworkCore;
using ShopzyWeb.Data;
using ShopzyWeb.Repository;
using ShopzyWeb.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// DATABASE
// ==============================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// ==============================
// RAZOR PAGES
// ==============================
builder.Services.AddRazorPages();

// ==============================
// SESSION (CART)
// ==============================
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ==============================
// AUTHENTICATION & AUTHORIZATION (ADMIN)
// ==============================
builder.Services.AddAuthentication("AdminCookie")
    .AddCookie("AdminCookie", options =>
    {
        options.LoginPath = "/Admin/Login";
        options.AccessDeniedPath = "/Admin/Login";
    });

builder.Services.AddAuthorization();

// ==============================
// REPOSITORY DEPENDENCY INJECTION
// ==============================
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ==============================
// BUILD APP
// ==============================
var app = builder.Build();

// ==============================
// MIDDLEWARE PIPELINE
// ==============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// 🔐 IMPORTANT ORDER
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// ==============================
// DB SEEDING
// ==============================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(db);
}

app.Run();
