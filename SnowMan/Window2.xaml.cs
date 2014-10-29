using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnowMan
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public static bool open = false;
        public Window2()
        {
            InitializeComponent();
            //
            //            
            bool m1 = Properties.Settings.Default.TopMost;
            bool m2 = Properties.Settings.Default.AutoRun;
            //
            if (m1 == true) checkBox1.IsChecked = true;
            else checkBox1.IsChecked = false;
            //
            if (m2 == true) checkBox2.IsChecked = true;
            else checkBox2.IsChecked = false;
            //
            open = true;
        }
        //
        // Нажата кнопка "ОК"
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool m1, m2;
            if (checkBox1.IsChecked == true)
            {
                m1 = true;
            }
            else m1=false;
            //
            if (checkBox2.IsChecked == true)
            {
                m2 = true;
            }
            else m2 = false;
            //
            Properties.Settings.Default.TopMost = m1;
            Properties.Settings.Default.AutoRun = m2;
            Properties.Settings.Default.Save();
            //
            try
            {
                if (Properties.Settings.Default.AutoRun)
                {
                    // Открываем нужную ветку в реестре    
                    // @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\"             
                    Microsoft.Win32.RegistryKey Key =
                        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
                    // добавляем первый параметр - название ключа   
                    // Второй параметр - это путь к    
                    // исполняемому файлу нашей программы.   
                    Key.SetValue("SnowMan", System.Windows.Forms.Application.ExecutablePath);
                    Key.Close();
                }
                else
                {
                    //удаляем   
                    Microsoft.Win32.RegistryKey key =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                        "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    key.DeleteValue("SnowMan", false);
                    key.Close();
                }
            }
            catch
            {
                MessageBox.Show("Возникла проблема при работе с реестром");
            }
            //            
            this.Close();
        }
        //
        // Закрытие формы
        private void Window_Closed(object sender, EventArgs e)
        {
            open = false;
        }
      
    }
}
