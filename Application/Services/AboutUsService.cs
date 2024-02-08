using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models.AboutUs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AboutUsService : IAboutUsService
    {
        #region Ctor 

        private readonly IAboutUsRepository _aboutUsRepository;

        public AboutUsService(IAboutUsRepository aboutUsRepository)
        {
            _aboutUsRepository = aboutUsRepository;
        }

        #endregion

        #region Admin Side 

        public async Task<AboutUs> GetAboutUs(CancellationToken cancellationToken)
        {
            return await _aboutUsRepository.GetAboutUs(cancellationToken);
        }

        public async Task<bool> AddOrEditAboutUs(AboutUs newAboutUs , CancellationToken cancellation)
        {
            #region Get Lastest About Us

            var oldAboutUs = await GetAboutUs(cancellation);

            #endregion

            #region Add OR Edit

            if (oldAboutUs != null)
            {
                oldAboutUs.Message1 = newAboutUs.Message1;
                oldAboutUs.Message2 = newAboutUs.Message2;

                _aboutUsRepository.UpdateAboutUs(oldAboutUs);
                await _aboutUsRepository.SaveChangesAsync(cancellation);
            }
            else
            {
                AboutUs aboutUs = new()
                {
                    Message1 = newAboutUs.Message1,
                    Message2 = newAboutUs.Message2
                };

                await _aboutUsRepository.AddAboutUs(aboutUs , cancellation);
                await _aboutUsRepository.SaveChangesAsync(cancellation);
            }

            #endregion

            return true;
        }

        #endregion
    }
}
