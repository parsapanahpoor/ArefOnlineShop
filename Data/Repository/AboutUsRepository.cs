using Data.Context;
using Domain.Interfaces;
using Domain.Models.AboutUs;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class AboutUsRepository : IAboutUsRepository
    {
        #region Ctor 

        private readonly ParsaWorkShopContext _context;

        public AboutUsRepository(ParsaWorkShopContext context)
        {
            _context = context;
        }

        #endregion

        #region General Methods

        public async Task AddAboutUs(AboutUs aboutUs , CancellationToken cancellation)
        {
            await _context.AboutUs.AddAsync(aboutUs);
        }

        public void UpdateAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Update(aboutUs);
        }

        public async Task SaveChangesAsync(CancellationToken cancellation)
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Admin Side 

        public async Task<AboutUs> GetAboutUs(CancellationToken cancellationToken)
        {
            return await _context.AboutUs.FirstOrDefaultAsync(p=> !p.IsDelete);
        }

        #endregion
    }
}
