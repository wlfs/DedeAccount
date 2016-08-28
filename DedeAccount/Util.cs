using System;
using System.Collections.Generic;
using System.Text;

namespace DedeAccount
{
    class Util
    {
        public static string RandomStr(int length)
        {
            string zz = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string zz1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random r = new Random();
            string result = zz1[r.Next(zz1.Length)].ToString();
            for (int i = 1; i < length; i++)
            {
                result += zz[r.Next(zz.Length)].ToString();
            }
            return result;
        }
    }
}
