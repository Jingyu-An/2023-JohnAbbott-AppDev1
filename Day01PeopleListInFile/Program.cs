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
        static string path = @"C:\Users\Administrator\source\repos\Jingyu-An\2023-JohnAbbott-AppDev1\Day01PeopleListInFile\people.txt";

        static void Main(string[] args)
        {
            ReadAllPeopleFromFile();

            bool running = true;

            while (running)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Add person info");
                Console.WriteLine("2. List persons info");
                Console.WriteLine("3. Find and list a person by name");
                Console.WriteLine("4. Find and list persons younger than age");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                if (!int.TryParse(choice, out int num) || num > 4 || num < 0)
                {
                    Console.WriteLine("Invalid choice try again.\n");
                    continue;
                }

                switch (num)
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

        static void ReadAllPeopleFromFile()
        {
            string line;
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    line = reader.ReadLine();

                    while (line != null)
                    {
                        Person person = new Person();

                        string[] info = line.Split(';');


                        if (!int.TryParse(info[1], out int age) || info[2] == null)
                        {
                            Console.WriteLine("Invalid value.");
                            line = reader.ReadLine();
                            continue;
                        }

                        person.setName(info[0]);
                        person.setAge(age);
                        person.setCity(info[2]);

                        people.Add(person);

                        line = reader.ReadLine();
                    }
                }
                //reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
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
                        writer.WriteLine($"{person.getName()};{person.getAge()};{person.getCity()}");
                    });
                }

                //writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Excetion: " + e.Message);
            }
        }

        static void AddPersonInfo()
        {
            Person person = new Person();
            Console.WriteLine("\nAdding a person.");

            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            person.setName(name);

            while (true)
            {
                Console.Write("Enter age: ");
                string strAge = Console.ReadLine();
                if (!int.TryParse(strAge, out int age) || age > 150 || age < 0)
                {
                    Console.WriteLine("Invalid age try again.(Age: 0 - 150)");
                    continue;
                }
                person.setAge(age);
                break;
            }

            Console.Write("Enter city: ");
            string city = Console.ReadLine();
            person.setCity(city);

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
                if (person.getName().Contains(partialName))
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
                    if (person.getAge() < maxAge)
                    {
                        Console.WriteLine(person);
                    }
                });
                break;
            }
            Console.WriteLine("\n");
        }

        public class Person
        {
            public string Name;
            public int Age;
            public string City;

            public string getName()
            {
                return Name;
            }

            public void setName(string name)
            {
                Name = name;
            }

            public int getAge()
            {
                return Age;
            }
            public void setAge(int age)
            {
                Age = age;
            }

            public void setCity(string city)
            {
                City = city;
            }
            public string getCity()
            {
                return City;
            }

            public override string ToString()
            {
                return $"{Name} is {Age} from {City}";
            }
        }
    }
}
