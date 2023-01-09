using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day01PeopleListInFile
{
    internal class Program
    {
        static List<Person> people = new List<Person>();

        static void Main(string[] args)
        {

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
                        running= false;
                        break;

                    case 1:
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                }
            }
        }

        static void AddPersonInfo()
        {

        }
        static void ListAllPersonsInfo()
        {

        }
        static void FindPersonByName()
        {

        }
        static void FindPersonYoungerThan()
        {

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
                return Name + " is " + Age + " from " + City;
            }
        }
    }
}
