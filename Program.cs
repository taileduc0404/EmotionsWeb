using CamXucWeb.Data;
using CamXucWeb.Interface;
using CamXucWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CamXucWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);



			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký IReviewService với ReviewService
            builder.Services.AddTransient<IReviewService, ReviewService>();



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

            //DataSeeding();
            app.Run();


            //void DataSeeding()
            //{
            //    using (var scope = app.Services.CreateScope())
            //    {
            //        var ReviewService = scope.ServiceProvider.GetRequiredService<IReviewService>();
            //        var exampleVm = new CamXucVM { Text = "This restaurant is so good" };
            //        ReviewService.AddReview(exampleVm, true);
            //    }
            //}
        }
	}
}