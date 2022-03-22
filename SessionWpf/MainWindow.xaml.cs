using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SessionWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            var userRole = DB.CmdScalar($"SELECT [ID_Role] FROM [User] WHERE [Login] = '{loginBox.Text}' AND [Password] = '{passwordBox.Password}'");
            if (userRole != null)
            {
                switch (userRole)
                {
                    case "1":
                        Window adminWindow = new WindowAdmin();
                        adminWindow.Show();
                        break;
                    case "2":
                        MessageBox.Show("Admin");
                        break;
                    case "3":
                        MessageBox.Show("Admin");
                        break;
                    case "4":
                        MessageBox.Show("Admin");
                        break;
                }
            }
            else MessageBox.Show("Такого пользователя не существует");
        }
    }
}
