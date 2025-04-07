using CW2.DAL.EF;
using CW2.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

string connStr = config.GetConnectionString("BookStoreDb");
connStr = connStr.Replace("|DbDir|", builder.Environment.ContentRootPath);

builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options
    .UseSqlServer(connStr)
    .UseLoggerFactory(
        LoggerFactory.Create(builder => builder.AddConsole().AddDebug()
    ))
);


// Dapper implementation with stored procedures customer
builder.Services.AddSingleton<ICustomerRepository>(s => new CustomerSPDapperRepository(connStr));


builder.Services.AddSingleton<IOrderRepository>(s => new OrderSPDapperRepository(connStr));

// Entity framework implementation without filter
//builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
