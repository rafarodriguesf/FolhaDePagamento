using FolhaDePagamento.Data;
using FolhaDePagamento.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "MyPolicy",
		policy =>
		{
			policy.WithOrigins("https://192.168.0.167",
				"https://localhost")
					.WithMethods("PUT", "DELETE", "GET", "POST")
					.AllowAnyHeader()
					.AllowAnyOrigin();
		});
});

// Habilita o MemoryCache
builder.Services.AddDistributedMemoryCache();
// Define configura��es padr�es de sess�o
builder.Services.AddSession(options =>
{
	options.Cookie.Name = ".FolhaDePagamento.Session";
	options.IdleTimeout = TimeSpan.FromSeconds(99999);
	//options.Cookie.HttpOnly = true; 
	options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages().AddMvcOptions(options =>
{
	options.MaxModelValidationErrors = 50;
	options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
		_ => "Este campo � Obrigat�rio!");
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BancoContext>(
	o => o.UseSqlServer(
			builder.Configuration.GetConnectionString("DataBase")
		)
	);

builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
builder.Services.AddScoped<IHorasTrabalhadasRepositorio, HorasTrabalhadasRepositorio>();
builder.Services.AddScoped<IFeriasRepositorio, FeriasRepositorio>();
builder.Services.AddScoped<IFuncionarioRepositorio, FuncionarioRepositorio>();
builder.Services.AddScoped<ICargoRepositorio, CargoRepositorio>();

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

app.UseSession();

app.UseCors();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
