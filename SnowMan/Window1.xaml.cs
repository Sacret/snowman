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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public static bool open = false;
        public Window1()
        {
            InitializeComponent();
            open = true;
        }
        //
        // Закрытие формы
        private void Window_Closed(object sender, EventArgs e)
        {
            open = false;
        }
    }
}
