using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Genarator
{
    public static class RandomNumberGenerator
    {
        public static String GetNumber()
        {
            Random rand = new Random();
            string num = "";
            for (int i = 0; i < 5; i++)
            {
                string number = rand.Next(0, 9).ToString();
                if (num.Contains(number) == false)
                    num += number;
                else
                    i--;
            }
            //listSource = num;
            return num;
        }
    }

}
