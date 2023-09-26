using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentalCarService.DbAccess;
using RentalCarService.Interfaces;
using RentalCarService.Mappers;
using RentalCarService.Services;
using RentalCarService.Validators;

namespace RentalCarService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IValidateCategories, ValidateCategories>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IValidateCountries, ValidateCountries>();
            services.AddTransient<IBrandsService, BrandsService>();
            services.AddTransient<IValidateBrands, ValidateBrands>();
            services.AddTransient<IBranchsService, BranchsService>();
            services.AddTransient<IValidateBranchs, ValidateBranches>();
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IValidateCar, ValidateCar>(); 
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IValidateUser, ValidateUser>();
            services.AddTransient<IExtraService, ExtraService>();
            services.AddTransient<IValidateExtra, ValidateExtra>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IValidateBooking, ValidateBooking>();
            services.AddTransient<IAvailabilityService, AvailabilityService>();
            services.AddTransient<IBookingMapper, BookingMapper>();
            services.AddTransient<IUserMapper, UserMapper>();
            services.AddTransient<ICarMapper, CarMapper>();
            services.AddTransient<IBrandsDBAccess, BrandsDBAccess>();
            services.AddTransient<ICategoriesDBAccess, CategoriesDBAccess>();
            services.AddTransient<IBookingDBAccess, BookingDBAccess>();
            services.AddTransient<IBranchesDBAccess, BranchesDBAccess>();
            services.AddTransient<ICarDBAccess, CarDBAccess>();
            services.AddTransient<IUserDBAccess, UserDBAccess>();
            services.AddTransient<ICountriesDBAccess, CountriesDBAccess>();
            services.AddTransient<IExtraDBAccess, ExtraDBAccess>();
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                opt.JsonSerializerOptions.Converters.Add(new DayOfWeekConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentalCarService", Version = "v1" });
            });

            string connectionString = Configuration.GetValue<string>("ConnectionStringDBContext");
            services.AddDbContext<RentalCarsDBContext>(DB => DB.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentalCarService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
