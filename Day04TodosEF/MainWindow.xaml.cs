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
using static Day04TodosEF.Todo;

namespace Day04TodosEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int id = 0;
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TodoListDbContext();
                LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                // Close();
                Environment.Exit(1);
            }
        }

        private void BtnAddTodo_Click(object sender, RoutedEventArgs e)
        {
            id++;
            string task = TbxTask.Text;
            int difficulty = (int)SliderDifficulty.Value;
            DateTime dueDate = DatepickerDueDate.DisplayDate;
            string status = ComboStatus.Text;

            Globals.dbContext.TodoList.Add(new Todo(id, task, difficulty, dueDate, status));
            Globals.dbContext.SaveChanges();
            LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();
        }

        private void BtnDeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            Todo todo = LvTodoList.SelectedItem as Todo;

            Globals.dbContext.TodoList.Remove(todo);
            Globals.dbContext.SaveChanges();
            LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();

        }
    }
}
