using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _41_размер
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    /// 
  
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            Captcha.Visibility = Visibility.Hidden;
            CaptchaAnswer.Visibility = Visibility.Hidden;
        }
        

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }
            if (Captcha.IsVisible == true && CaptchaAnswer.Text == "")
            {
                MessageBox.Show("Введите CAPTCHA");
                return;
            }

            User user = Muzafarov41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            

            if (Captcha.IsVisible == true)
            {
                if (user != null && CaptchaAnswer.Text == captchaOneWord.Text + captchaTwoWord.Text + captchaThreeWord.Text + captchaFourWord.Text)
                {
                    Manager.MainFrame.Navigate(new ProductPage(user));
                    LoginBox.Text = "";
                    PasswordBox.Text = "";
                    Captcha.Visibility = Visibility.Hidden;
                    CaptchaAnswer.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Неверный логин, пароль или CAPTCHA");
                    LoginBtn.IsEnabled = false;
                    LoginBox.Text = "";
                    PasswordBox.Text = "";
                    CaptchaAnswer.Text = "";
                    await Task.Delay(10000);
                    LoginBtn.IsEnabled = true;

                }
            }
            else
            {
                if (user != null)
                {
                    Manager.MainFrame.Navigate(new ProductPage(user));
                    LoginBox.Text = "";
                    PasswordBox.Text = "";
                    Captcha.Visibility = Visibility.Hidden;
                    CaptchaAnswer.Visibility = Visibility.Hidden;
                }
                else
                {
                    string CaptchaSymbols = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890";
                    MessageBox.Show("Неверный логин или пароль");

                    Captcha.Visibility = Visibility.Visible;
                    CaptchaAnswer.Visibility = Visibility.Visible;
                    Random CaptchaSym = new Random();
                    captchaOneWord.Text = CaptchaSymbols[CaptchaSym.Next(0, 76)].ToString();
                    captchaTwoWord.Text = CaptchaSymbols[CaptchaSym.Next(0, 76)].ToString();
                    captchaThreeWord.Text = CaptchaSymbols[CaptchaSym.Next(0, 76)].ToString();
                    captchaFourWord.Text = CaptchaSymbols[CaptchaSym.Next(0, 76)].ToString();

                    LoginBtn.IsEnabled = false;
                    LoginBox.Text = "";
                    PasswordBox.Text = "";
                    await Task.Delay(10000);
                    LoginBtn.IsEnabled = true;
                }
            }
        }
        private void GuestLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ProductPage(null));
        }
    }
}
