using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using photolib;
using comlib;
using System.Windows.Media.Imaging;

namespace Test_or_Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            ConfigHelper configHelper = new ConfigHelper();
            Lockscreen lockscreen = new Lockscreen(configHelper.GetValue("LOCKSCREEN", "ASSETS"), configHelper.GetValue("LOCKSCREEN", "OUTPATH"));
            lockscreen.OutputScreen();
            lockscreen.DeleteMoblieLock();
            //Console.WriteLine(lockscreen.GetAlockScreenValue(40).ImageName);
            //System.IO.File.Copy("C:\\Users\\xiexy\\Pictures\\QQ截图20170410201857.png", "C:\\Users\\xiexy\\Pictures\\test\\QQ截图20170410201857.png", true);
            //lockscreen.ErgodicDirector(configHelper.GetValue("LOCKSCREEN", "ASSETS"));
            //Version currentVersion = Environment.OSVersion.Version;
            //Console.WriteLine(currentVersion.Major);
            //Console.WriteLine("Your Windows Version Major is : " + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            //string strJson = httpHelper.GetHttpData("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=3");
            //Console.WriteLine(strJson);
            //string strJson2 = @"{ ""student"": { ""Name1"": ""小明"" , ""Name2"": ""小花"" , ""Name3"": ""小红""} }";
            //JObject jsonObj = JObject.Parse(strJson);
            //Console.WriteLine(jsonObj["images"][2]["copyright"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name2"].ToString());
            //Console.ReadLine();


            /*
            Console.ReadLine();
            Photos photos = new Photos(0, 8);
            int[] aa = new int[8];
            for(int i = 0; i < 8; i++)
            {
                Console.WriteLine(photos.GetAphotoValue(i).Copyright);
            }
            */
            Console.WriteLine("执行结束！");
            Console.ReadLine();
        }                           
    }
}