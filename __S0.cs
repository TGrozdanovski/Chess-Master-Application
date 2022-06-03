using System;
using System.Linq;

namespace Chess_Bot_2
{

    internal class __S0
    {
        private static Random _r = new Random();

        public static string _er = "";

        public static string _PA(string _s, string _ppo)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string text2 = __d(_s, text);
            string text3 = "";
            int num = 0;
            for (int i = 0; i < _ppo.Length; i++)
            {
                num = text.IndexOf(_ppo[i]);
                text3 += text2[num];
            }
            _er = text3;
            return text3;
        }

        private static string __d(string qx28, string qx24)
        {
            string text = "";
            while (qx28.Length > 0)
            {
                text += qx28[0];
                qx28 = qx28.Replace(qx28[0].ToString(), "");
            }
            string text2 = qx24;
            for (int i = 0; i < text.Length; i++)
            {
                text2 = text2.Replace(text[i].ToString(), "");
            }
            return text + text2;
        }

        public static string qx31(int length)
        {
            return new string((from s in Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", length)
                               select s[_r.Next(s.Length)]).ToArray());
        }
    }
}
