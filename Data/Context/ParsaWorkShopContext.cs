#region Usings

using Domain.Models.Blog;
using Domain.Models.Comment;
using Domain.Models.ContactUs;
using Domain.Models.Order;
using Domain.Models.Permissions;
using Domain.Models.Product;
using Domain.Models.SiteSetting;
using Domain.Models.Slider;
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

        #endregion

        #region Order

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<ReturnedProducts> ReturnedProducts { get; set; }
        public DbSet<ReturnedProductType> ReturnedProductTypes { get; set; }
        public DbSet<FinancialTransactionType> FinancialTransactionType { get; set; }

        #endregion

        #region Site Setting

        public DbSet<SiteSetting> SiteSettings { get; set; }

        #endregion

        #region Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


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
