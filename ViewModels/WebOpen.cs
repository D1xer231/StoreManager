using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace StoreManager.ViewModels
{
    public partial class WebOpen
    {
        [RelayCommand]
        private void LinkOpen(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                try
                {
                    string currentDir = Directory.GetCurrentDirectory();
                    string path = Path.Combine(currentDir, url);
                    path = Path.GetFullPath(path);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    MessageBox.Show($"Unable to open the link or file.\n\nError while {url} opening");
                }
            }
        }
    }
}
