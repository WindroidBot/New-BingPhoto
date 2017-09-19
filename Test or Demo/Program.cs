using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;
using photolib;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace Test_or_Demo
{
    class Program
    {

        static void Main(string[] args)
        {
            HttpHelper httpHelper = new HttpHelper();
            
            //string strJson = httpHelper.GetHttpData("http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=3");
            //Console.WriteLine(strJson);
            //string strJson2 = @"{ ""student"": { ""Name1"": ""小明"" , ""Name2"": ""小花"" , ""Name3"": ""小红""} }";
            //JObject jsonObj = JObject.Parse(strJson);
            //Console.WriteLine(jsonObj["images"][2]["copyright"].ToString());
            //Console.WriteLine(jsonObj["student"]["Name2"].ToString());
            //Console.ReadLine();

            ConfigHelper configHelper = new ConfigHelper();
            Setting setting = new Setting();

            configHelper.Initialise_ini();
            //configHelper.SetValue("AUTOSET", "true");

            Photo photo = new Photo(-1, 1);
            //Console.WriteLine(photo.HDUrl);

            httpHelper.DownLoadPhoto(photo.HDUrl);


            //Console.WriteLine(httpHelper.DirectGetJsonValue(0,1,0, "copyright"));
            //Photo testphoto = new Photo(0, 1);
            //Console.WriteLine(testphoto.RequestStr);
            //Console.WriteLine(testphoto.Startdate);
            //Console.WriteLine(testphoto.WXGAUrl);
            //Console.WriteLine(testphoto.HDUrl);

            /*
            Console.ReadLine();
            Photos photos = new Photos(0, 8);
            int[] aa = new int[8];
            for(int i = 0; i < 8; i++)
            {
                Console.WriteLine(photos.GetAphotoValue(i).Copyright);
            }
            */









            Console.ReadLine();
        }                           
    }
}