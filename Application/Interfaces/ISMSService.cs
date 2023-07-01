#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace Application.Interfaces
{
    public interface ISMSService
    {
        Task<Kavenegar.Core.Models.SendResult?> SendLookupSMS(string receptor, string token, string token2, string token3, string template);
        Task<Kavenegar.Core.Models.SendResult?> SendSimpleSMS(string receptor, string message);
    }
}

