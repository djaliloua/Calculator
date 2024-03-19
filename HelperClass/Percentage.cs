﻿using System.Data;
using System.Globalization;

namespace HelperClass
{
    public static class Percentage
    {
        private static char[] operators = { '+','/', '-', '*'};
        private static string GetValues(string text, int len)
        {
            return text[..(text.Length - len)];
        }
        public static string MakePercentage(string text)
        {

            (string, int) res;
            res = GetPercentage(text);
            string r = GetValues(text, res.Item2);
            return r + res.Item1;
        }
        private static (string, int) GetPercentage(string text)
        {
            var list = text.Split(operators);
            int intValue;
            string result;
            string len = list[list.Length - 1];
            if(int.TryParse(len, out intValue))
            {
                result = decimal.Divide(intValue, 100).ToString();
            }
            else
                result = decimal.Divide(decimal.Parse(len, CultureInfo.InvariantCulture), 100).ToString();

            return (result.Replace(",", "."), len.Length);
        }
        public static bool IsValideExpression(string text)
        {
            object value = new DataTable().Compute(text, null);
            return (value != null);
        }
    }
}