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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace SnowMan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //
        // Переменные и константы
        private WindowState fCurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return fCurrentWindowState; }
            set { fCurrentWindowState = value; }
        }
        //
        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private ContextMenu TrayMenu = null;
        //
        // Главное окно приложения
        public MainWindow()
        {
            InitializeComponent();
            //
            // Загрузка настроек
            LoadSettings();
            //
            // Создание анимации снежинок
            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.0;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            //
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            Storyboard.SetTargetName(myDoubleAnimation, snow1.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(10));
            Storyboard.SetTargetName(myDoubleAnimation, snow2.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(3));
            Storyboard.SetTargetName(myDoubleAnimation, snow3.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(7));
            Storyboard.SetTargetName(myDoubleAnimation, snow4.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            Storyboard.SetTargetName(myDoubleAnimation, snow5.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(8));
            Storyboard.SetTargetName(myDoubleAnimation, snow6.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(15));
            Storyboard.SetTargetName(myDoubleAnimation, snow7.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(9));
            Storyboard.SetTargetName(myDoubleAnimation, snow8.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(22));
            Storyboard.SetTargetName(myDoubleAnimation, snow9.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
            //
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(20));
            Storyboard.SetTargetName(myDoubleAnimation, snow10.Name);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Ellipse.OpacityProperty));
            myStoryboard.Begin(this);
        }
        //
        // переопределяем обработку первичной инициализации приложения
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e); // базовый функционал приложения в момент запуска
            createTrayIcon(); // создание нашей иконки
        }        
        //
        // Создание иконки трея
        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            { // только если мы не создали иконку ранее
                TrayIcon = new System.Windows.Forms.NotifyIcon(); // создаем новую
                TrayIcon.Icon = Properties.Resources.box;
                // обратите внимание, за ресурсом с картинкой мы лезем в свойства проекта, а не окна,
                // поэтому нужно указать полный namespace
                TrayIcon.Text = "Snowman"; // текст подсказки, всплывающей над иконкой
                TrayMenu = Resources["TrayMenu"] as ContextMenu; // а здесь уже ресурсы окна и тот самый x:Key
                // сразу же опишем поведение при щелчке мыши, о котором мы говорили ранее
                // это будет просто анонимная функция, незачем выносить ее в класс окна
                TrayIcon.Click += delegate(object sender, EventArgs e)
                {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        // по левой кнопке показываем или прячем окно
                        ShowHideMainWindow(sender, null);
                    }
                    else
                    {
                        // по правой кнопке (и всем остальным) показываем меню
                        TrayMenu.IsOpen = true;
                        Activate(); // нужно отдать окну фокус, см. ниже
                    }
                };
                result = true;
            }
            else
            { // все переменные были созданы ранее
                result = true;
            }
            TrayIcon.Visible = true; // делаем иконку видимой в трее
            return result;
        }
        //
        // Показать / скрыть окно
        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false; // спрячем менюшку, если она вдруг видима
            if (IsVisible) // если окно видно на экране
            {
                // прячем его
                Hide();
                // меняем надпись на пункте меню
                (TrayMenu.Items[0] as MenuItem).Header = "Показать";
            }
            else// а если не видно
            { 
                // показываем
                Show();
                // меняем надпись на пункте меню
                (TrayMenu.Items[0] as MenuItem).Header = "Скрыть";
                //
                WindowState = CurrentWindowState;
                Activate(); // обязательно нужно отдать фокус окну,
                // иначе пользователь сильно удивится, когда увидит окно
                // но не сможет в него ничего ввести с клавиатуры
            }
        }
        //
        // переопределяем встроенную реакцию на изменение состояния окна
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e); // системная обработка
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                // если окно минимизировали, просто спрячем
                Hide();
                // и поменяем надпись на менюшке
                (TrayMenu.Items[0] as MenuItem).Header = "Показать";
            }
            else
            {
                // в противном случае запомним текущее состояние
                CurrentWindowState = WindowState;
            }
        }
        //     
        // Перемещение формы необычной формы
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        } 
        //
        // Настройки       
        private void ShowHideMainWindow0(object sender, RoutedEventArgs e)
        {
            if (!Window2.open)
            {
                Window2 wnd = new Window2();
                wnd.ShowDialog();
                LoadSettings();
            }
        }
        //
        // О программе       
        private void ShowHideMainWindow1(object sender, RoutedEventArgs e)
        {
            if (!Window1.open)
            {
                Window1 wnd = new Window1();
                wnd.ShowDialog();
            }
        }
        //
        // Выход
        private void ShowHideMainWindow2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }    
        //
        // Загрузка параметров
        void LoadSettings()
        {
            this.Topmost = Properties.Settings.Default.TopMost;            
        }        
    }
}
