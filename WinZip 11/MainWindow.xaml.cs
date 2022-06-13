using MicaWPF.Controls;
using MicaWPF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

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
            Loaded += (sender, args) =>
            {
                
                WPFUI.Appearance.Watcher.Watch(
                  this,                           // Window class
                  WPFUI.Appearance.BackgroundType.Mica, // Background type
                  true                            // Whether to change accents automatically
                );
                Random random = new Random();
                int tip_num = random.Next(0, win_fun_fact_list.Length);
                WinFunFact.Content = win_fun_fact_list[tip_num];
            };
        }

        public static string[] zip_file_name;
        public static string destination_path;
        public static string[] selected_files;
        public static List<String> zfn = new List<String>();
        public static string file_exists_path;

        public static string[] win_fun_fact_list =
        {
            "Did you know how to effective work? Try use Snap Windows!",
            "Have you tried use sticker on your background?",
            "Have you check on new outlook design? If you didn't try check out!"
        };

        public static string[] win_fun_fact_list_korean =
        {
            "효율적인 작업을 하는 방법을 알고 있나요?, 스냅으로 윈도우 작업해보세요.",
            "배경화면에 스티커 기능을 사용해보셨나요?",
            "혹시 새로운 아웃룩 디자인 보셨나요? 한 번 확인해보세요!"
        };

        

        public static string[] fun_with_progress_ring =
        {
            "Need some help?",
            "Why you click me for no reason?",
            "If you want to start to extract/archive try click somewhere not me.",
            "Have you try some new feature about Windows 11?",
            "...",
            "Feels bored?, You can touch some grass :)",
            "Stop abusing me, I'm not a mouse!",
            "Why are you doing this?",
            "Stop it!",
            "you are so rude on me!"
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /// <summary>
            /// Select Zipped File Method
            /// </summary>
            zfn.Clear();
            MultiFileList.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Zip Files (*.zip)|*.zip|7z Files (*.7z)|*.7z|All Files (*.*)|*.*";
            ofd.Title = "Select your zipped file.";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string file in ofd.FileNames)
                {
                    zfn.Add(file);
                    MultiFileList.Items.Add(Path.GetFileName(file));
                }
            }
            FL.Content = "These File will be extract.";
            RingChan.Visibility = Visibility.Hidden;
            FunWithMe.Visibility = Visibility.Hidden;
            // set file_exists_path to 
            file_exists_path = Path.GetFullPath(ofd.FileName.Replace(".zip", ""));

            // if folder path is already exists.
            if (Directory.Exists((file_exists_path)))
            {
                FileAlreadyExists fae = new FileAlreadyExists();
                fae.Show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destination_path = fbd.SelectedPath;
                destination_path = Path.GetFullPath(destination_path);
            }
        }
        static bool is_extract = true;
        static int count = 0;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // TODO Make Extract Function
            //Body1.Visibility = Visibility.Visible;
            //Body2.Visibility = Visibility.Visible;
            //FileN.Visibility = Visibility.Visible;
            //FolderN.Visibility = Visibility.Visible;
            Button1.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Visible;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            if(MultiFileList.Items.Count == 0)
            {
                RingChan.Visibility = Visibility.Visible;
                FunWithMe.Visibility = Visibility.Visible;
            }
            //TS1.Visibility = Visibility.Visible;
            //this.Height = 523;
            count += 1;
            is_extract = true;
            if (Language.SelectedIndex == 1)
            {
                TB1.Content = "     Extract Compressed(Zipped) Folders";
                Head.Content = "Select a Destination and Extract Files";
                Body1.Content = "Select compressed file:";
                Body2.Content = "Files will be extracted to this folder:";
                FL.Content = "Please Select The Zip File.";
            }
            else if (Language.SelectedIndex == 2)
            {
                TB1.Content = "    압축 해제 프로그램";
                Head.Content = "압축 풀 경로와 압축 풀 파일을 선택하세요.";
                Body1.Content = "압축 파일 선택하세요: ";
                Body2.Content = "압축 풀 경로를 선택해주세요: ";
                FL.Content = "Zip 파일을 선택하세요.";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // TODO Make Archive Function
            //Body1.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Visible;
            Button4.Visibility = Visibility.Visible;
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            //Body2.Visibility = Visibility.Hidden;
            //FileN.Visibility = Visibility.Hidden;
            //FolderN.Visibility = Visibility.Hidden;
            //TS1.Visibility = Visibility.Hidden;
            //this.Height = this.Height - 200;
            count += 1;
            is_extract = false;
            if (Language.SelectedIndex == 1)
            {
                TB1.Content = "     Archive Files";
                Head.Content = "Welcome to Archive!";
                FL.Content = "Please Select File/Folder What you want to archive";
            }
            else if (Language.SelectedIndex == 2)
            {
                TB1.Content = "   파일 압축하기";
                Head.Content = "파일 압축하는 시스템에 오신걸 환영합니다.";
                FL.Content = "파일이나 폴더를 선택하시고 압축할 준비를 해주세요.";
            }

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            MultiFileList.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selected_files = ofd.FileNames;
                foreach (string file in ofd.FileNames)
                {
                    MultiFileList.Items.Add(Path.GetFileName(file));
                }
            }
            FL.Content = "These List will be archive.";
            RingChan.Visibility = Visibility.Hidden;
            FunWithMe.Visibility = Visibility.Hidden;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Zip File";
            sfd.Filter = "Zip Files (*.zip)|*.zip|7z Files (*.7z)|*.7z|All Files (*.*)|*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string archive_path = Path.GetFullPath(sfd.FileName);
                using (ZipArchive archive = ZipFile.Open(archive_path, ZipArchiveMode.Update))
                {
                    foreach (string file in selected_files)
                    {
                        archive.CreateEntryFromFile(file, Path.GetFileName(file));

                    }
                }
            }

        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < MultiFileList.Items.Count; i++)
            {

                ZipFile.ExtractToDirectory(Path.GetFullPath(zfn[i]), destination_path);

            }
            if (count >= 69)
            {
                count = 0;
                WinFunFact.Content = "Thanks!";
                if (Language.SelectedIndex == 1)
                {
                    WinFunFact.Content = "감사합니다.";
                }
            }
            else
            {
                count = 0;
            }

            if (TS1.IsChecked == true)
            {
                if (Directory.Exists(destination_path))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        Arguments = destination_path,
                        FileName = "explorer.exe"
                    };
                    Process.Start(startInfo);
                }
            }
        }

        private void ThemeChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var themeService = ThemeService.GetCurrent();
            var tag_num = ThemeChanger.SelectedIndex;
            if (tag_num == 1)
                themeService.EnableMica(this, MicaWPF.BackdropType.Acrylic);
            else if (tag_num == 2)
                themeService.EnableMica(this, MicaWPF.BackdropType.Mica);
            else if (tag_num == 3)
                themeService.EnableMica(this, MicaWPF.BackdropType.Tabbed);
            else if (tag_num == 4)
                themeService.EnableMica(this, MicaWPF.BackdropType.None);
        }

        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Language.SelectedIndex == 1)
            {
                string win_fun = WinFunFact.Content.ToString();
                if (win_fun.Equals(win_fun_fact_list_korean[0]))
                {
                    WinFunFact.Content = win_fun_fact_list[0];
                }
                else if (win_fun.Equals(win_fun_fact_list_korean[1]))
                {
                    WinFunFact.Content = win_fun_fact_list[1];
                }
                else if (win_fun.Equals(win_fun_fact_list_korean[2]))
                {
                    WinFunFact.Content = win_fun_fact_list[2];
                }

                if (is_extract)
                {
                    TB1.Content = "     Extract Compressed(Zipped) Folders";
                    Head.Content = "Select a Destination and Extract Files";
                    Body1.Content = "Select compressed file:";
                    Body2.Content = "Files will be extracted to this folder:";
                    FL.Content = "Please Select The Zip File.";
                }

                if (count >= 69)
                {
                    WinFunFact.Content = "did you just feel like boring to you?";
                    if (count >= 75)
                    {
                        WinFunFact.Content = "I mean... you know I like find some easter egg.";
                    }
                    if (count >= 80)
                    {
                        WinFunFact.Content = "Is your work name... Switcher?";
                    }
                    if (count >= 90)
                    {
                        WinFunFact.Content = "Stop Switch! You harming your window!";
                    }
                }
                if (WinFunFact.Content.Equals("Thanks!"))
                {
                    WinFunFact.Content = "Not again, Why are you doing this?";
                }

            }
            else if (Language.SelectedIndex == 2)
            {
                string win_fun = WinFunFact.Content.ToString();
                if (win_fun.Equals(win_fun_fact_list[0]))
                {
                    WinFunFact.Content = win_fun_fact_list_korean[0];
                }
                else if (win_fun.Equals(win_fun_fact_list[1]))
                {
                    WinFunFact.Content = win_fun_fact_list_korean[1];
                }
                else if (win_fun.Equals(win_fun_fact_list[2]))
                {
                    WinFunFact.Content = win_fun_fact_list_korean[2];
                }

                if (is_extract)
                {
                    TB1.Content = "    압축 해제 프로그램";
                    Head.Content = "압축 풀 경로와 압축 풀 파일을 선택하세요.";
                    Body1.Content = "압축 파일 선택하세요: ";
                    Body2.Content = "압축 풀 경로를 선택해주세요: ";
                    FL.Content = "Zip 파일을 선택하세요.";
                }

                if (count >= 69)
                {
                    WinFunFact.Content = "혹시 지루하시는 건가요?";
                    if (count >= 75)
                    {
                        WinFunFact.Content = "저는 그니까... 저도 이스터에그를 찾는 거 좋아해요.";
                    }
                    if (count >= 80)
                    {
                        WinFunFact.Content = "당신의 직업이 스위처인가요?";
                    }
                    if (count >= 90)
                    {
                        WinFunFact.Content = "그만 전환하세요. 당신이 윈도우를 해치고 있어요!";
                    }
                }
                if (WinFunFact.Content.Equals("감사합니다."))
                {
                    WinFunFact.Content = "또 그러시네. 도대체 왜 이러세요?";
                }


            }
        }

        private void ProgressRing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var secret = new Secret();
            Random r = new Random();
            int r_i = r.Next(0, 8);
            if (r_i >= 7)
            {
                secret.ShowDialog();
                secret = null;
            }
            else
            {
                FunWithMe.Content = fun_with_progress_ring[r_i];
                secret = null;
            }
            
        }

        private void ProgressRing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var secret = new Secret();
            Random r = new Random();
            int r_i = r.Next(0, 12);
            if (r_i >= 11)
            {
                secret.Show();
                secret = null;
                FunWithMe.Content = "I told you, It may crash your computer!";
            }
            else
            {
                
                try
                {
                    FunWithMe.Content = fun_with_progress_ring[r_i];
                }
                catch(IndexOutOfRangeException)
                {
                    FunWithMe.Content = fun_with_progress_ring[0];
                }
                secret = null;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }
    }
}
