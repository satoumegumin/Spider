using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using SpiderCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClawerSpider
{
    class Program
    {
        private static bool GetDoString(string sstr, params string[] str)
        {
            if (!str.Contains(sstr))
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine();
            

            Console.WriteLine("全部完毕");
        }
    }
}
