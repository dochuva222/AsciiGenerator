using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AsciiArtGenerator.Tools
{
    public static class MyTools
    {
        public static BitmapImage BytesToImage(byte[] bytes)
        {
            using(MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
        }
        public static BitmapImage BytesToImage(byte[] bytes, int asciiHeight, int asciiWidth)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.DecodePixelHeight = asciiHeight;
                image.DecodePixelWidth = asciiWidth;
                image.EndInit();
                return image;
            }
        }
    }
}
