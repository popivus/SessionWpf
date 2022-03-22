using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SessionWpf
{
    /// <summary>
    /// Логика взаимодействия для WindowEditUser.xaml
    /// </summary>
    public partial class WindowEditUser : Window
    {
        private int IdUser;
        public WindowEditUser(int idUser)
        {
            InitializeComponent();
            IdUser = idUser;
            var user = DB.FillDataSet($"SELECT * FROM [User] WHERE [ID_User] = {IdUser}").Tables[0];
            var d = user.Rows[0].ItemArray;
            surnameBox.Text = d[1].ToString();
            nameBox.Text = d[2].ToString();
            middlenameBox.Text = d[3].ToString();
            loginBox.Text = d[8].ToString();
            passwordBox.Text = d[9].ToString();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(surnameBox.Text) && !string.IsNullOrWhiteSpace(nameBox.Text) && !string.IsNullOrWhiteSpace(loginBox.Text) && !string.IsNullOrWhiteSpace(passwordBox.Text))
            {
                DB.CmdScalar($"UPDATE [User] SET [Surname] = '{surnameBox.Text}', [Name] = '{nameBox.Text}', [Middlename] = '{middlenameBox.Text}', [Login] = '{loginBox.Text}', [Password] = '{passwordBox.Text}' WHERE [ID_User] = {IdUser}");
                (Owner as WindowAdmin).RefreshDataGrid();
                Close();
            }
            else MessageBox.Show("Заполните все поля");
        }
    }
}
