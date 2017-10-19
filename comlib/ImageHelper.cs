using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace comlib
{
    public class ImageHelper
    {
        /// <summary>
        /// 从指定URL将图片读入内存缓存，并返回该BitmapImage对象，使用完成后使用BitmapImage.Freeze()释放对象
        /// </summary>
        /// <param name="bitImagePath">图片的URL</param>
        /// <returns>BitmapImage对象</returns>
        public BitmapImage BitImageHelper(string bitImagePath)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream(File.ReadAllBytes(bitImagePath));

            return bitmapImage;
        }
    }
}
