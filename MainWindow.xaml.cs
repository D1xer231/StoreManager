using StoreManager.Models;
using StoreManager.Services;
using StoreManager.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StoreManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppDbContext? db;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WebOpen();
            DateAndTimeSet();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new AppDbContext();

            var loginService = new LoginService(
                UserNameTextBox,
                PasswordBox,
                context
            );

            bool success = loginService.Login(
                UserNameTextBox,
                PasswordBox
            );

            if (success)
            {
                MessageBox.Show("Login successful!");
                ShowScreen(ScreenType.Home);
                WelcomeUserNameText.Text = context.Users.FirstOrDefault(u => u.FirstName == UserNameTextBox.Text)?.FirstName.ToUpper();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // temporary after change to more right one for db
        }
        private void ToCheckoutBtn_Click(object sender, RoutedEventArgs e) => ShowScreen(ScreenType.Checkout);
        private void ShowScreen(ScreenType type)
        {
            LoginGrid.Visibility = Visibility.Hidden;
            HomeGrid.Visibility = Visibility.Hidden;
            CheckoutGrid.Visibility = Visibility.Hidden;

            switch (type)
            {
                case ScreenType.Login: LoginGrid.Visibility = Visibility.Visible; break;
                case ScreenType.Home: HomeGrid.Visibility = Visibility.Visible; break;
                case ScreenType.Checkout: CheckoutGrid.Visibility = Visibility.Visible; break;
            }
        }
        public enum ScreenType
        {
            Login,
            Home,
            Checkout
        }
        private void DateAndTimeSet()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += (s, e) =>
            {
                ClockText.Text = DateTime.Now.ToString("HH:mm:ss");
                DateText.Text = DateTime.Now.ToString("dd/MM/yyyy");
            };
            timer.Start();
        }
    }
}