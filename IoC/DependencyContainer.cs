using Application.Interfaces;
using Application.Services;
using Data.Repository;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)
        {
            #region Application Layer

            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IPermissionService, PermissionService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<ILocationService, LocationService>();
            service.AddScoped<IFinancialTransactionService, FinancialTransactionService>();
            service.AddScoped<IReturendProductsService, ReturendProductsService>();

            #endregion


            #region Data Layer

            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IPermissionRepository, PermissionRepository>();
            service.AddScoped<IBlogRepository, BlogRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<ICommentRepository, CommentRepository>();
            service.AddScoped<ILocationRepository, LocationRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
            service.AddScoped<IReturendProductsRepository, ReturendProductsReposiotry>();

            #endregion

        }
    }
}
