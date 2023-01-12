using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        const string filePath = @"../../";
        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromFile("people.txt");
            LvPeople.ItemsSource = peopleList;
        }

        private void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            //if (!ArePersonInputsValid()) return;
            string name = TbxName.Text;
            string error = null;
            if (Person.IsNameValid(name, out error))
            {
                 // okay: no need to validate again
                if(int.TryParse(TbxAge.Text, out int age) && Person.IsAgeValid(age, out error)) {
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
            LblStatus.Text = "Person added!";
            ResetFields();
        }

        private void ResetFields()
        {
            TbxName.Clear();
            TbxAge.Clear();
        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            peopleList.RemoveAt(LvPeople.SelectedIndex);
            LvPeople.SelectedItem = null;
            LvPeople.Items.Refresh();
            LblStatus.Text = "Person deleted!";
            ResetFields();
        }
        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            string error = null;
            if (Person.IsNameValid(name, out error))
            {
                 // okay: no need to validate again
                if(int.TryParse(TbxAge.Text, out int age) && Person.IsAgeValid(age, out error)) {
                    peopleList.ElementAt(LvPeople.SelectedIndex).Name = name;
                    peopleList.ElementAt(LvPeople.SelectedIndex).Age = age;
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
            LblStatus.Text = "Person updated!";
            ResetFields();
        }

        private void LvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvPeople.SelectedItem != null)
            {
                TbxName.Text = peopleList.ElementAt(LvPeople.SelectedIndex).Name;
                TbxAge.Text = $"{peopleList.ElementAt(LvPeople.SelectedIndex).Age}";
                LblStatus.Text = $"Selected person : {LvPeople.SelectedIndex+1}";
            }
        }

        private void LoadDataFromFile(string fileName) // call when window is loaded
        {
            // do your best with validation
            // data stored semicolon-separated, one per line:  Jerry;33
            string line;
            try
            {
                if (!File.Exists(filePath +fileName)) 
                {
                    return;
                }

                using (StreamReader reader = new StreamReader(filePath + fileName))
                {
                    line = reader.ReadLine();
                    if (line == null) return;

                    while (line != null)
                    {
                        try
                        {
                            string[] infoPerson = line.Split(';');
                            if (infoPerson.Length != 2)
                            {
                                throw new FormatException("Invalid number of data");
                            }

                            string error = null;
                            if (!Person.IsNameValid(infoPerson[0], out error))
                            {
                                throw new FormatException("Invalid Name Fromat");
                            }
                            if (!int.TryParse(infoPerson[1], out int age) || !Person.IsAgeValid(age, out error))
                            {
                                throw new FormatException("Invalid Age Fromat");
                            }

                            Person person = new Person(infoPerson[0], age);
                            peopleList.Add(person);

                            line = reader.ReadLine();
                        }
                        catch (Exception e) when (e is FormatException)
                        {
                            MessageBox.Show(this, e.Message, "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                LblStatus.Text = "Saved file loaded!";
            }
            catch (Exception e) when (e is IOException || e is SystemException)
            {
                MessageBox.Show(this, "Error : Fail reading file", "Fail to load", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SaveDataToFile(string fileName) // call when window is closing
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath + fileName))
                {
                    peopleList.ForEach(person =>
                    {
                        writer.WriteLine($"{person.Name};{person.Age}");
                    });
                }
            } catch (Exception e) when (e is IOException || e is SystemException)
            {
                MessageBox.Show(this, "Error : Fail writing file", "Fail to save", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Data File (*.data)|*.data|All File (*.*)|*.*|Text File (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    peopleList.ForEach(person =>
                    {
                        writer.WriteLine($"{person.Name};{person.Age}");
                    });
                }
            }
        }

        private void MiSortByAge_Click(object sender, RoutedEventArgs e)
        {
            peopleList = peopleList.OrderBy(person => person.Age).ToList();
            LvPeople.ItemsSource = peopleList;
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Age";
        }

        private void MiSortByName_Click(object sender, RoutedEventArgs e)
        {
            peopleList = peopleList.OrderBy(person => person.Name).ToList();
            LvPeople.ItemsSource = peopleList;
            LvPeople.Items.Refresh();
            LblStatus.Text = "Sorted by Name";
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile("people.txt");
            Environment.Exit(0);
        }
    }
}
