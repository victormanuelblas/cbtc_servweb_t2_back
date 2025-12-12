using Microsoft.Extensions.DependencyInjection;
using servweb1_t2.Application.Services;
using servweb1_t2.Application.Interfaces;
using servweb1_t2.Application.Mappings;

namespace servweb1_t2.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}