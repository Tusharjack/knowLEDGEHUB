using Dapper;
using KnowledgeHub.Data;
using KnowledgeHub.Repositories;
using Npgsql;

DefaultTypeMap.MatchNamesWithUnderscores = true;

var builder = WebApplication.CreateBuilder(args);

// Debug startup
Console.WriteLine("========== STARTUP ==========");
Console.WriteLine($"Environment : {builder.Environment.EnvironmentName}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine($"Connection String Found : {!string.IsNullOrEmpty(connectionString)}");

try
{
    using var testConnection = new NpgsqlConnection(connectionString);
    testConnection.Open();
    Console.WriteLine("✅ Successfully connected to Supabase.");
}
catch (Exception ex)
{
    Console.WriteLine("❌ DATABASE CONNECTION FAILED");
    Console.WriteLine(ex.ToString());
}

// Add MVC services
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register Database Connection Factory
builder.Services.AddSingleton<DbConnectionFactory>();

// Register Repositories
builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();