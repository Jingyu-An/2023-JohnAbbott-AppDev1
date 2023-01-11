using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Day01PeopleListInFile.Program;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        static List<Person> people = new List<Person>();
        const string path = @"C:\Users\Administrator\source\repos\Jingyu-An\2023-JohnAbbott-AppDev1\Day01PeopleListInFile\people.txt";

        static void Main(string[] args)
        {
            ReadAllPeopleFromFile();

            bool running = true;

            while (running)
            {
                int choice = getMenuChoice();

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\nGood bye!\n");
                        running = false;
                        break;

                    case 1:
                        AddPersonInfo();
                        break;

                    case 2:
                        ListAllPersonsInfo();
                        break;

                    case 3:
                        FindPersonByName();
                        break;

                    case 4:
                        FindPersonYoungerThan();
                        break;

                }
            }

            SaveAllPeopleToFile();
            Console.ReadLine();
        }

        private static int getMenuChoice()
        {
            Console.Write("What do you want to do?\n" +
                   "1.Add person info\n" +
                   "2.List persons info\n" +
                   "3.Find and list a person by name\n" +
                   "4.Find and list persons younger than age\n" +
                   "0.Exit\n" + "Choice: ");
            return (int.TryParse(Console.ReadLine(), out int choice)) ? choice : -1;
        }

        static void ReadAllPeopleFromFile()
        {
            string line;
            try
            {
                if (!File.Exists(path))
                {
                    return;
                }
                using (StreamReader reader = new StreamReader(path))
                {
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        try
                        {
                            Person person = new Person();

                            string[] info = line.Split(';');

                            if (info.Length != 3)
                            {
                                throw new FormatException("Invalid number of items");
                            }

                            person.Name = info[0];
                            person.Age = int.Parse(info[1]);
                            person.City = info[2];

                            people.Add(person);

                            line = reader.ReadLine();
                        }
                        catch (Exception e) when (e is FormatException || e is ArgumentException)
                        {
                            Console.WriteLine("Error: " + e.Message);
                        }
                    }
                }
                //reader.Close();
            }
            catch (Exception e) when (e is IOException || e is SystemException)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
        }

        static void SaveAllPeopleToFile()
        {
            try
            {
                //FileStream fs = new FileStream(path, FileMode.Append);
                using (StreamWriter writer = new StreamWriter(path))
                {
                    people.ForEach(person =>
                    {
                        writer.WriteLine($"{person.Name};{person.Age};{person.City}");
                    });
                }

                //writer.Close();
            }
            catch (Exception e) when (e is IOException || e is SystemException)
            {
                Console.WriteLine("Error writing file: " + e.Message);
            }
        }

        static void AddPersonInfo()
        {
            Person person = new Person();
            Console.WriteLine("\nAdding a person.");

            while (true)
            {
                try
                {
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    person.Name = name;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error : " + e.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter age: ");
                    int age = int.Parse(Console.ReadLine());
                    person.Age = age;
                    break;
                } 
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Enter city: ");
                    string city = Console.ReadLine();
                    person.City = city;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);

                }
            }

            people.Add(person);
            Console.WriteLine("Person added.\n");
        }

        static void ListAllPersonsInfo()
        {
            Console.WriteLine("\nListing all persons");
            people.ForEach(person =>
            {
                Console.WriteLine(person);
            });
            Console.WriteLine("\n");
        }
        static void FindPersonByName()
        {
            Console.WriteLine("\nEnter partial person name:");
            string partialName = Console.ReadLine();

            people.ForEach(person =>
            {
                if (person.Name.Contains(partialName))
                {
                    Console.WriteLine(person);
                }
            });
            Console.WriteLine("\n");
        }

        static void FindPersonYoungerThan()
        {
            Console.WriteLine("\n");
            while (true)
            {
                Console.WriteLine("Enter maximum age:");
                string strAge = Console.ReadLine();

                if (!int.TryParse(strAge, out int maxAge))
                {
                    Console.WriteLine("Invalid age try again.");
                    continue;
                }

                people.ForEach(person =>
                {
                    if (person.Age < maxAge)
                    {
                        Console.WriteLine(person);
                    }
                });
                break;
            }
            Console.WriteLine("\n");
        }

    }
}
