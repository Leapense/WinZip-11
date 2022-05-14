using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Diagnostics;
using MicaWPF.Controls;

namespace WinZip_11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MicaWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string zip_file_name;
        public static string destination_path;
        public static string[] selected_files;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// Select Zipped File Method
            /// </summary>

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Zip Files (*.zip)|*.zip|7z Files (*.7z)|*.7z|All Files (*.*)|*.*";
            ofd.Title = "Select your zipped file.";
            
            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                zip_file_name = ofd.FileName;
                FileN.Text = Path.GetFullPath(zip_file_name);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destination_path = fbd.SelectedPath;
                FolderN.Text = Path.GetFullPath(destination_path);
            }
        }

        private void FolderN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(FolderN.Text.ToLower().Equals("con/con") || FolderN.Text.ToLower().Equals("aux/aux") || FolderN.Text.ToLower().Equals("prn/prn"))
            {
                NoEaster.Content = "Ayyy!! You found the error of Windows 98!";
                Secret s = new Secret();
                s.Show();
            }
            else if(FolderN.Text.ToLower().Contains("porn"))
            {
                NoEaster.Content = "Police! Police! Arrest this horny person!";
            }
            else if (FolderN.Text.ToLower().Contains("egg"))
            {
                NoEaster.Content = "Why you storing egg on this computer?";
            }
            else
            {
                NoEaster.Content = "P.S. There is no easter-egg";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // TODO Make Extract Function
            TB1.Header = "     Extract Compressed(Zipped) Folders";
            Head.Content = "Select a Destination and Extract Files";
            Body1.Content = "Select compressed file:";
            FileN.Placeholder = "Select your Zip File or Enter the full path";
            Body2.Content = "Files will be extracted to this folder:";
            FolderN.Placeholder = "Select your Destination Path";
            Body2.Visibility = Visibility.Visible;
            FolderN.Visibility = Visibility.Visible;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // TODO Make Archive Function
            TB1.Header = "     Archive Files";
            Head.Content = "Select Files and Archive Folder Name";
            Body1.Content = "Please Choose one or multiple files.";
            Button3.Visibility = Visibility.Visible;
            Button4.Visibility = Visibility.Visible;
            Body2.Visibility = Visibility.Hidden;
            FolderN.Placeholder = "Enter the folder path";
        }
        
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selected_files = ofd.FileNames;
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Zip File";
            sfd.Filter = "Zip Files (*.zip)|*.zip|7z Files (*.7z)|*.7z|All Files (*.*)|*.*";
            if(sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string archive_path = Path.GetFullPath(sfd.FileName);
                using(ZipArchive archive = ZipFile.Open(archive_path, ZipArchiveMode.Update))
                {
                    foreach(string file in selected_files)
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }
            }
            
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ZipFile.ExtractToDirectory(FileN.Text, FolderN.Text);
            if(TS1.IsChecked == true)
            {
                if(Directory.Exists(FolderN.Text))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = FolderN.Text,
                        FileName = "explorer.exe"
                    };
                    Process.Start(startInfo);
                }
            }
        }
    }
}
