using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;
using System.Xml;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Test_or_Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            HttpHelper httpHelper = new HttpHelper();
            string strJson = httpHelper.GetHttpData("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1");
            Console.WriteLine(strJson);


            string strJson2 = @"{""images"":{""startdate"":""20170916"",""fullstartdate"":""201709161600"",""enddate"":""20170917""}}";






             JObject jsonObj = JObject.Parse(strJson2);

            //Dictionary<string, string> _dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(strJson);

            Console.WriteLine(jsonObj["images"]["startdate"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name2"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name3"].ToString());
            Console.ReadLine();
        }                           
    }
}