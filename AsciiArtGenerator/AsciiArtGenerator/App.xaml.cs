using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AsciiArtGenerator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AddRegistryKey(@"pngfile\shell\Open with AsciiArtGenerator\command\", string.Empty, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"AsciiArtGenerator.exe %1").Replace("\\", "\\\\"));
            AddRegistryKey(@"jpegfile\shell\Open with AsciiArtGenerator\command\", string.Empty, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"AsciiArtGenerator.exe %1").Replace("\\", "\\\\"));
        }

        private void AddRegistryKey(string path, string name, string value)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(path, true);
            if (key == null)
                key = Registry.ClassesRoot.CreateSubKey(path);
            key.SetValue(name, value);
            key.Close();
        }
    }
}
