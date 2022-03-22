using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Логика взаимодействия для WindowAdmin.xaml
    /// </summary>
    public partial class WindowAdmin : Window
    {
        public WindowAdmin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        public void RefreshDataGrid()
        {
            var users = DB.FillDataSet("SELECT * FROM [User]").Tables[0];
            usersGrid.Items.Clear();
            foreach (DataRow row in users.Rows)
            {
                var d = row.ItemArray;
                usersGrid.Items.Add(new UserShow(d[0].ToString(), $"{d[1]} {d[2]} {d[3]}", d[8].ToString(), d[9].ToString()));
            }
        }

        public class UserShow
        {
            public UserShow(string id, string fIO, string login, string password)
            {
                Id = id;
                FIO = fIO;
                Login = login;
                Password = password;
            }

            public string Id { get; set; }
            public string FIO { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItem != null)
            {
                DB.CmdScalar($"DELETE FROM [User] WHERE [ID_User] = {((UserShow)usersGrid.SelectedItem).Id}");
                RefreshDataGrid();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersGrid.SelectedItem != null)
            {
                Window window = new WindowEditUser(int.Parse(((UserShow)usersGrid.SelectedItem).Id));
                window.Owner = this;
                window.ShowDialog();
            }
        }

        private void positionsButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new WindowPositions();
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
