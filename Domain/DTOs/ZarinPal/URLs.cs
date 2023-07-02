using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs.ZarinPal
{
    public class URLs
    {
        public const String gateWayUrl = "https://www.zarinpal.com/pg/StartPay/";
        public const String requestUrl = "https://api.zarinpal.com/pg/v4/payment/request.json";
        public const String verifyUrl = "https://api.zarinpal.com/pg/v4/payment/verify.json";


    }
}
