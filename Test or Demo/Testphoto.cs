using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;

namespace Test_or_Demo
{
    public class Testphoto
    {
        private int startdate;//开始日期
        public enum countrylist { _JP, _CN, _IN, _DE, _FR, _GB, _BR, _CA, _US, _AU };//国家和地区
        private string requestStr;//请求字符串
        private string hDUrl;//1920x1080 分辨率图片URL
        private string wXGAUrl;//1366x768 分辨率图片URL
        private string copyright;//版权与说明信息

        public int Startdate { get => startdate; set => startdate = value; }
        public string RequestStr { get => requestStr; set => requestStr = value; }
        public string HDUrl { get => hDUrl; set => hDUrl = value; }
        public string WXGAUrl { get => wXGAUrl; set => wXGAUrl = value; }
        public string Copyright { get => copyright; set => copyright = value; }

        public Testphoto(int idx, int n)
        {
            HttpHelper httpHelper = new HttpHelper();
            string requestJson = httpHelper.GetHttpData(httpHelper.GetRequestUrl(idx, n));
            this.startdate = int.Parse(httpHelper.GetJsonValue(requestJson, 0, "startdate"));
            this.requestStr = httpHelper.GetRequestUrl(idx, n);
            this.hDUrl = "www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "url");
            this.wXGAUrl = "www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "urlbase") + "_1366x768.jpg";
            this.copyright = httpHelper.GetJsonValue(requestJson, 0, "copyright");
        }
    }
}
