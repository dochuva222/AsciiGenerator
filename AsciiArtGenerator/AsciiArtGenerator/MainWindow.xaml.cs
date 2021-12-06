using AsciiArtGenerator.Model;
using AsciiArtGenerator.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsciiArtGenerator
{
    public partial class MainWindow : Window
    {
        private string[] asciiChars = { "@", "%", "#", "*", "+", "=", "-", ":", ".", " " };
        private byte[] imageBytes;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BSelectPicture_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                imageBytes = File.ReadAllBytes(dialog.FileName);
                iSelectedPicture.Source = MyTools.BytesToImage(imageBytes);
            }
        }

        private void BGenerateAscii_Click(object sender, RoutedEventArgs e)
        {
            var selectedImage = ResizeImage((BitmapImage)iSelectedPicture.Source);
            var str = ConvertToAscii(selectedImage);
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
                File.WriteAllText(dialog.FileName, str);
        }

        private BitmapImage ResizeImage(BitmapImage image)
        {
            int asciiHeight = image.PixelHeight / 4;
            int asciiWidth = image.PixelWidth / 1;
            return MyTools.BytesToImage(imageBytes, asciiHeight, asciiWidth);
        }

        private string ConvertToAscii(BitmapImage image)
        {
            StringBuilder sb = new StringBuilder();
            byte[] bytes = BitmapSourceToArray(image);
            PixelColor[] pixels = new PixelColor[bytes.Length / 4];
            int currentPixel = 0;

            for (int i = 0; i < bytes.Length; i += 4)
            {
                pixels[currentPixel] = new PixelColor() { Blue = bytes[i], Green = bytes[i + 1], Red = bytes[i + 2], Alpha = bytes[i + 3] };
                currentPixel++;
            }

            currentPixel = 0;

            for (int h = 0; h < image.PixelHeight; h++)
            {
                for (int w = 0; w < image.PixelWidth; w++)
                {
                    int asciiCharIndex = (pixels[currentPixel].Red * (asciiChars.Length - 1) / 255);
                    sb.Append(asciiChars[asciiCharIndex]);
                    currentPixel++;
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
        {
            // Stride = (width) x (bytes per pixel)
            int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];
            bitmapSource.CopyPixels(pixels, stride, 0);
            return pixels;
        }
    }
}
