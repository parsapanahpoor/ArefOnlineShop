using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DTOs.ZarinPal
{
    public class VerifyParameters
    {
        public string amount {   set; get; }
        public string merchant_id {   set; get; }
        public string authority {   set; get; }
    }
}
