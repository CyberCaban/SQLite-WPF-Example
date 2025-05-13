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

namespace SSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBState DBState = new DBState();
        List<TodoItem> TodoItems = new List<TodoItem>();
        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
        }
        private void UpdateUI()
        {
            TodoItems = DBState.GetItems();
            foreach (TodoItem item in TodoItems)
            {
                Console.WriteLine(item);
            }
            TodoListBox.ItemsSource = null;
            TodoListBox.ItemsSource = TodoItems;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox.Text.Trim()))
            {
                var newItem = new TodoItem { Title = TextBox.Text, IsCompleted = false };
                DBState.InsertOrReplace(newItem);
                TextBox.Text = "";
                TextBox.Focus();
                UpdateUI();
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            var item = checkbox.DataContext as TodoItem;
            DBState.InsertOrReplace(item);
            UpdateUI();

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TodoListBox.SelectedItem != null)
            {
                DBState.DeleteItem(TodoListBox.SelectedItem as TodoItem);
                UpdateUI();
            }
        }
    }
}
