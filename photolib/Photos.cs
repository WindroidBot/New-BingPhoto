using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using comlib;

namespace photolib
{
    public struct Aphoto
    {
        private int jsonindex;//结果集索引
        private int enddate;//日期
        private string hDUrl;//1920x1080分辨率URL
        private string wXGAUrl;//1366x768分辨率URL
        private string copyright;//版权与说明信息

        public int Jsonindex { get => jsonindex; set => jsonindex = value; }
        public int Startdate { get => enddate; set => enddate = value; }
        public string HDUrl { get => hDUrl; set => hDUrl = value; }
        public string WXGAUrl { get => wXGAUrl; set => wXGAUrl = value; }
        public string Copyright { get => copyright; set => copyright = value; }
    }

    public class Photos
    {
        private int idx;//相对时间参数对应API参数中的idx
        private int resultSum;//返回数据的总量对应API参数中的n
        public enum Countrylist { _JP, _CN, _IN, _DE, _FR, _GB, _BR, _CA, _US, _AU };//国家和地区
        public enum AphotoKey { _Jsonindex, _startdate, _hDUrl, _wXGAUrl, _copyright };//Aphoto的字段
        private string requestStr;//请求字符串
        private List<Aphoto> photocontainer = new List<Aphoto>();

        public int Idx { get => idx; set => idx = value; }
        public int ResultSum { get => resultSum; set => resultSum = value; }
        public string RequestStr { get => requestStr; set => requestStr = value; }
        internal List<Aphoto> Photocontainer { get => photocontainer; set => photocontainer = value; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">API中的idx字段（相对时间参数）</param>
        /// <param name="resultSum">API中的n字段（返回的结果数目）</param>
        public Photos(int idx, int resultSum)
        {
            this.resultSum = resultSum;
            HttpHelper httpHelper = new HttpHelper();
            this.requestStr = httpHelper.GetRequestUrl(idx, resultSum);
            string requestJson = httpHelper.GetHttpData(this.requestStr);
            //批量增加数据集合
            for(int n = 0; n < this.resultSum; n++)
            {
                Aphoto aphoto = new Aphoto
                {
                    Jsonindex = n,
                    Startdate = int.Parse(httpHelper.GetJsonValue(requestJson, n, "enddate")),
                    HDUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, n, "url"),
                    WXGAUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, n, "urlbase") + "_1366x768.jpg",
                    Copyright = httpHelper.GetJsonValue(requestJson, n, "copyright"),
                };
                //Console.WriteLine(aphoto.Startdate + aphoto.HDUrl + aphoto.Copyright);
                photocontainer.Add(aphoto);
            }

        }

        /// <summary>
        /// 访问结果集结构体的访问器
        /// </summary>
        /// <param name="index">结果集的索引</param>
        /// <returns>Aphoto结构体</returns>
        public Aphoto GetAphotoValue(int index)
        {
            return photocontainer[index];
        }

        /// <summary>
        /// 批量下载所有图片
        /// </summary>
        /// <returns>批量下载所有图片</returns>
        public void BeathDownloadImage()
        {
            for(int index = 0; index < 8; index++)
            {
                Thread thread = new Thread(DownloadThreadHDUrl);
                thread.Start(index);
            }
        }

        /// <summary>
        /// 批量下载1080p图片的子线程，只允许被BeathDownloadImage()调用
        /// </summary>
        private void DownloadThreadHDUrl(Object index)
        {
            int n = (int)index;
            HttpHelper httpHelper = new HttpHelper();
            try
            {
                httpHelper.DownLoadPhoto(GetAphotoValue(n).HDUrl);
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("【system】第" + n + "张图片下载失败");
            }
            Console.WriteLine("【system】第" + n + "张图片下载成功");
        }
    }
}
