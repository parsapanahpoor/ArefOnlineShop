﻿#region Usings

using Domain.Models.AboutUs;
using Domain.Models.Blog;
using Domain.Models.Comment;
using Domain.Models.ContactUs;
using Domain.Models.Discount;
using Domain.Models.FavoriteProducts;
using Domain.Models.Order;
using Domain.Models.Permissions;
using Domain.Models.Product;
using Domain.Models.SiteSetting;
using Domain.Models.Slider;
using Domain.Models.UserCommentAboutSite;
using Domain.Models.Users;
using Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Linq;

#endregion

namespace Data.Context
{
    public class ParsaWorkShopContext : DbContext
    {
        #region Ctor

        public ParsaWorkShopContext(DbContextOptions<ParsaWorkShopContext> options) : base(options)
        {

        }

        #endregion

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<Locations> Locations { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion

        #region Wallet

        public DbSet<Wallet> Wallet { get; set; }

        public DbSet<WalletData> WalletData { get; set; }

        #endregion

        #region Blog

        public DbSet<Blog> Blog { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogSelectedCategory> BlogSelectedCategories { get; set; }
        public DbSet<VideoSelectedCategory> VideoSelectedCategory { get; set; }

        #endregion

        #region Comment

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Comment> ProductType { get; set; }

        #endregion

        #region Slider

        public DbSet<Slider> Slider { get; set; }

        #endregion

        #region ContactUs

        public DbSet<ContactUs> ContactUs { get; set; }

        #endregion

        #region Product

        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<ProductSelectedCategory> ProductSelectedCategory { get; set; }
        public DbSet<Product> product { get; set; }
        public DbSet<ProductFeature> ProductFeature { get; set; }
        public DbSet<ProductGallery> ProductGallery { get; set; }
        public DbSet<ProductsSize> ProductsSizes { get; set; }
        public DbSet<ProductColor> ProductColors{ get; set; }
        public DbSet<ProductSelectedSize> ProductSelectedSizes{ get; set; }
        public DbSet<ProductSelectedColors> ProductSelectedColors{ get; set; }
        
        #endregion

        #region Favorite Products

        public DbSet<FavoriteProducts> FavoriteProducts{ get; set; }

        #endregion

        #region Order

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<ReturnedProducts> ReturnedProducts { get; set; }
        public DbSet<ReturnedProductType> ReturnedProductTypes { get; set; }
        public DbSet<FinancialTransactionType> FinancialTransactionType { get; set; }
        public DbSet<OrderCancelationRequestDetail> OrderCancelationRequestDetail { get; set; }

        #endregion

        #region Site Setting

        public DbSet<SiteSetting> SiteSetting { get; set; }

        #endregion

        #region About Us

        public DbSet<AboutUs> AboutUs { get; set; }

        #endregion

        #region Discount

        public DbSet<DiscountCode> DiscountCodes { get; set; }

        public DbSet<DiscountCodeSelectedFromUser>  DiscountCodeSelectedUsers { get; set; }

        #endregion

        #region Users Comments About Site

        public DbSet<UsersCommentsAboutSite> UsersCommentsAboutSites { get; set; }

        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            #region Seed Data

            #region Product Type Seed Data

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                ProductTypeId = 1,
                ProductTypeTitle = "product",
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                ProductTypeId = 2,
                ProductTypeTitle = "blog",
            });

            modelBuilder.Entity<ProductType>().HasData(new ProductType
            {
                ProductTypeId = 3,
                ProductTypeTitle = "Video",
            });

            #endregion

            #endregion

            #region QueryFilter

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);

            modelBuilder.Entity<Role>()
            .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Blog>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<BlogCategory>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Video>()
                  .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Comment>()
                  .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Slider>()
                  .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<ProductCategories>()
                .HasQueryFilter(r => !r.IsDelete);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(r => !r.IsDelete);

            #endregion

            base.OnModelCreating(modelBuilder);
        }


        #endregion
    }
}
