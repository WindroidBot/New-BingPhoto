using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;
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
            /*
            string strJson = httpHelper.GetHttpData("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=3");
            Console.WriteLine(strJson);
            //string strJson2 = @"{ ""student"": { ""Name1"": ""小明"" , ""Name2"": ""小花"" , ""Name3"": ""小红""} }";
            JObject jsonObj = JObject.Parse(strJson);
            Console.WriteLine(jsonObj["images"][2]["copyright"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name2"].ToString());
            Console.ReadLine();

            ConfigHelper configHelper = new ConfigHelper();
            Setting setting = new Setting();
            configHelper.Initialise_ini(setting.GetOldWallpaperPath(), setting.GetMyDocumentsPath() + "\\bingphoto", setting.GetRunPath(), false, false);
            Console.WriteLine(configHelper.GetValue("OLDWALLPATH"));
            configHelper.SetValue("AUTOSET", "true");
            */

            Console.WriteLine(httpHelper.DirectGetJsonValue(0,1,0, "copyright"));
            Testphoto testphoto = new Testphoto(0, 1);
            Console.WriteLine(testphoto.RequestStr);
            Console.WriteLine(testphoto.Startdate);
            Console.WriteLine(testphoto.WXGAUrl);
            Console.WriteLine(testphoto.HDUrl);









            Console.ReadLine();
        }                           
    }
}