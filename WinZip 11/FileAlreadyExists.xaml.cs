using MicaWPF.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace WinZip_11
{
    /// <summary>
    /// Interaction logic for FileAlreadyExists.xaml
    /// </summary>
    public partial class FileAlreadyExists : MicaWindow
    {
        public FileAlreadyExists()
        {
            InitializeComponent();
            FolderPath.Content = MainWindow.file_exists_path;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = MainWindow.file_exists_path,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
            Close();
        }
    }
}
