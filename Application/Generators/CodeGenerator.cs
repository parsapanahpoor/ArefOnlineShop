using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Generators
{
    public static class CodeGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static int GenerateMobileActiveCode()
        {
            return new Random().Next(100000, 999999);
        }
    }
}
