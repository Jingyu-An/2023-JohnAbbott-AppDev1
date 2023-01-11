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
using System.Xml.Linq;

namespace Day03ListGridViewPeople
{
    public partial class MainWindow : Window
    {
        List<Person> peopleList = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            LvPeople.ItemsSource = peopleList;
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            //if (!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            string error = null;
            if (Person.IsNameValid(name, out error))
            {
                int.TryParse(TbxAge.Text, out int age); // okay: no need to validate again
                if(Person.IsAgeValid(age, out error)) {
                    peopleList.Add(new Person(name, age));
                    LvPeople.Items.Refresh(); // tell ListView data has changed
                }
                else
                {
                    MessageBox.Show(this, "Age must be 0-150", "Invalid Age", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } 
            else
            {
                MessageBox.Show(this, "Name must be 2-50 characters long, no semicolons", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //ResetFields();
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void LoadDataFromFile() // call when window is loaded
        {
            // do your best with validation
            // data stored semicolon-separated, one per line:  Jerry;33

        }

        private void SaveDataToFile() // call when window is closing
        {
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
