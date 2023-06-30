using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Genarator
{
    public class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
