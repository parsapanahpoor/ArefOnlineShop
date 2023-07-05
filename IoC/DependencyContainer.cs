using Application.Interfaces;
using Application.Services;
using Application.Services.Implementation;
using Application.Services.Interfaces;
using Application.SiteServices;
using Data.Repository;
using DoctorFAM.Data.Repository;
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
            service.AddScoped<IWalletService, WalletService>();
            service.AddScoped<ISliderService, SliderService>();
            service.AddScoped<IUsersCommentAboutSiteService, UsersCommentAboutSiteService>();
            service.AddScoped<IDiscountCodeService, DiscountCodeService>();
            service.AddScoped<IFavoriteProductsService, FavoriteProductsService>();
            service.AddScoped<ISiteSettingService, SiteSettingService>();

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
            service.AddScoped<IWalletRepository, WalletRepository>();
            service.AddScoped<IWalletRepository, WalletRepository>();
            service.AddScoped<ISliderRepository, SliderRepository>();
            service.AddScoped<IUsersCommentAboutSiteRepository, UsersCommentAboutSiteRepository>();
            service.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
            service.AddScoped<IFavoriteProductRepository, FavoriteProductRepository>();
            service.AddScoped<ISiteSettingRepsitory, SiteSettingRepsitory>();

            #endregion
        }
    }
}
