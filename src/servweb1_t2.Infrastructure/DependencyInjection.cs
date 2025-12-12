using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using servweb1_t2.Domain.Ports.Out;
using servweb1_t2.Infrastructure.Persistence.Context;
using servweb1_t2.Infrastructure.Persistence.Repositories;

namespace servweb1_t2.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var host = Environment.GetEnvironmentVariable("BD_HOST");
            var port = Environment.GetEnvironmentVariable("DB_PORT") ;
            var database = Environment.GetEnvironmentVariable("DB_NAME");
            var user = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

            var connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};";

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 0))
                )
            );

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}