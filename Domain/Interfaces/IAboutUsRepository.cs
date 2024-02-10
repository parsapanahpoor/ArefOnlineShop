using Domain.Models.AboutUs;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IAboutUsRepository
    {
        #region General Methods

        Task AddAboutUs(AboutUs aboutUs, CancellationToken cancellation);

        void UpdateAboutUs(AboutUs aboutUs);

        Task SaveChangesAsync(CancellationToken cancellation);

        #endregion

        #region Admin Side 

        Task<AboutUs> GetAboutUs(CancellationToken cancellationToken);

        #endregion
    }
}
