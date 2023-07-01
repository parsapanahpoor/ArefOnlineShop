#region Usings

using Application.Interfaces;
using Data.Context;
using Kavenegar.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.SiteServices;
using Application.StaticTools;

#endregion

namespace Application.Services
{
    public class SMSService : ISMSService
    {
        #region Ctor

        private ParsaWorkShopContext _context;
        private readonly IConfiguration _configuration;

        public SMSService(ParsaWorkShopContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #endregion

        public async Task<SendResult?> SendLookupSMS(string receptor, string token, string token2, string token3, string template)
        {
            var apikey = _configuration["kavenegar:apikey"];

            try
            {
                Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apikey);

                var result = await api.VerifyLookup(receptor, token, token2, token3, template);

                return result;
            }
            catch (Kavenegar.Core.Exceptions.ApiException ex)
            {
                await ExeptionLog.LogError(ex);
            }
            catch (Kavenegar.Core.Exceptions.HttpException ex)
            {
                await ExeptionLog.LogError(ex);
            }

            return null;
        }

        public async Task<SendResult?> SendSimpleSMS(string receptor, string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_configuration["kavenegar:apikey"]);

            //var result = await api.Send("10008663", receptor, message);
            var result = await api.Send("90004034", receptor, message);

            return result;
        }
    }
}
