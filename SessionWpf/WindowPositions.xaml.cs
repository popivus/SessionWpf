using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для WindowPositions.xaml
    /// </summary>
    public partial class WindowPositions : Window
    {
        public WindowPositions()
        {
            InitializeComponent();
        }

        private void salaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!"1234567890.".Contains(e.Text))
            {
                e.Handled = true;
                return;
            }
        }

        public void RefreshDataGrid()
        {
            var positions = DB.FillDataSet("SELECT * FROM [Position]").Tables[0];
            positionsGrid.Items.Clear();
            foreach (DataRow row in positions.Rows)
            {
                var d = row.ItemArray;
                positionsGrid.Items.Add(new PositionShow(d[0].ToString(), d[1].ToString(), d[2].ToString()));
            }
        }

        private void positionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (positionsGrid.SelectedItem != null)
            {
                var post = (PositionShow)positionsGrid.SelectedItem;
                nameBox.Text = post.Name;
                salaryBox.Text = post.Salary;
            }
        }

        private class PositionShow
        {
            public PositionShow(string iD_Position, string name, string salary)
            {
                ID_Position = iD_Position;
                Name = name;
                Salary = salary;
            }

            public string ID_Position { get; set; }
            public string Name { get; set; }
            public string Salary { get; set; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameBox.Text) && !string.IsNullOrWhiteSpace(salaryBox.Text))
            {
                DB.CmdScalar($"INSERT INTO [Position] ([Name], [Salary]) VALUES ('{nameBox.Text}', {salaryBox.Text})");
                RefreshDataGrid();
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (positionsGrid.SelectedItem != null)
            {
                DB.CmdScalar($"DELETE FROM [Position] WHERE [ID_Position] = {((PositionShow)positionsGrid.SelectedItem).ID_Position}");
                RefreshDataGrid();
            }
        }
    }
}
