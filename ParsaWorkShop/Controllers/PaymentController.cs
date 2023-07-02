#region Using

using Application.Extensions;
using Application.Interfaces;
using Application.Services.Interfaces;
using Application.StaticTools;
using Domain.DTOs.ZarinPal;
using Domain.Models.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParsaWorkShop.Web.Controllers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DoctorFAM.Web.Controllers
{
    [Authorize]
    public class PaymentController : SiteBaseController
    {
        #region Ctor

        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public PaymentController(IUserService userService, IWalletService walletService)
        {
            _userService = userService;
            _walletService = walletService;
        }

        #endregion

        #region Paymnet Method

        public async Task<IActionResult> PaymentMethod(GatewayType gatewayType, int amount, string description, string returURL , ulong? orderId)
        {
            #region Get User By Id

            var user = await _userService.GetUserByIdAsync(User.GetUserId());
            if (user == null) return NotFound();

            #endregion

            #region Online Payment

            try
            {
                using (var client = new HttpClient())
                {
                    RequestParameters parameters = new RequestParameters(PathTools.merchant, amount.ToString(), description, returURL, user.PhoneNumber, "");

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jo = JObject.Parse(responseBody);
                    string errorscode = jo["errors"].ToString();

                    JObject jodata = JObject.Parse(responseBody);
                    string dataauth = jodata["data"].ToString();

                    if (dataauth != "[]")
                    {
                        string authority = jodata["data"]["authority"].ToString();

                        string gatewayUrl = URLs.gateWayUrl + authority;

                        #region Create Wallet With False Finally

                        await _walletService.CreateNewWalletTransactionForRedirextToTheBankPortal(user.UserId , amount , gatewayType , authority.Trim() , description , requestId);

                        #endregion

                        return Redirect(gatewayUrl);
                    }
                    else
                    {
                        return BadRequest("error " + errorscode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #endregion

            return View();
        }

        #endregion

        #region Payment Result 

        [HttpGet("PaymentResult/{IsSuccess}/{refId}", Name = "PaymentResult")]
        public async Task<IActionResult> PaymentResult(bool IsSuccess , string? refId)
        {
            ViewBag.IsSuccess = IsSuccess;
            ViewBag.refId = refId;

            return View();
        }

        #endregion
    }
}
