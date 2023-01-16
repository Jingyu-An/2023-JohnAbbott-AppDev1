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

namespace Day04TodosEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            try
            {
                string task = TbxTask.Text;
                int difficulty = (int)SliderDifficulty.Value;
                DateTime dueDate = DatepickerDueDate.SelectedDate != null
                    ? DatepickerDueDate.SelectedDate.Value
                    : DatepickerDueDate.DisplayDate;
                Todo.StatusEnum status = (Todo.StatusEnum)ComboStatus.SelectedIndex;

                Globals.dbContext.TodoList.Add(new Todo(task, difficulty, dueDate, status));
                Globals.dbContext.SaveChanges();
                LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();
            } 
            catch (AggregateException ex)
            {
                MessageBox.Show(this, ex.Message, "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SystemException ex)
            {
                MessageBox.Show(this, "Error reading from database\n" + ex.Message, "Fatal error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            Todo todo = LvTodoList.SelectedItem as Todo;

            Globals.dbContext.TodoList.Remove(todo);
            Globals.dbContext.SaveChanges();
            LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();

        }

        private void LvTodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todo todo = LvTodoList.SelectedItem as Todo;
            if (todo != null)
            {
                TbxTask.Text = todo.Task;
                SliderDifficulty.Value = todo.Difficulty;
                DatepickerDueDate.SelectedDate = todo.DueDate;
                ComboStatus.SelectedIndex = (int)todo.Status;
            }
        }

        private void BtnUpdateTodo_Click(object sender, RoutedEventArgs e)
        {
            Todo todo = LvTodoList.SelectedItem as Todo;

            string task = TbxTask.Text;
            int difficulty = (int)SliderDifficulty.Value;
            DateTime dueDate = DatepickerDueDate.SelectedDate.Value;
            int status = ComboStatus.SelectedIndex;

            Todo updateTodo = Globals.dbContext.TodoList.Find(keyValues: todo.Id);

            updateTodo.Task = task;
            updateTodo.Difficulty = difficulty;
            updateTodo.DueDate = dueDate;
            updateTodo.Status = (Todo.StatusEnum)status;

            Globals.dbContext.SaveChanges();
            LvTodoList.ItemsSource = Globals.dbContext.TodoList.ToList();

        }

        private void LvTodoList_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu m = new ContextMenu();
            MenuItem delete = new MenuItem();
            delete.Header= "Delete";
            
        }
    }
}
