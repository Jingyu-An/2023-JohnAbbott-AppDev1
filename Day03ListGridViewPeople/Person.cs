using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day03ListGridViewPeople
{
    internal class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Person(string name, int age)
        {
            Name = name;      
            Age= age;
        }

        public static bool IsNameValid(string name, out string error)
        {
            error = null;
            Regex regex = new Regex(@"^[a-zA-Z]{1,50}[^;]");
            return regex.IsMatch(name); // FIXME: Name must be 2-50 characters long, no semicolons
        }

        public static bool IsAgeValid(int age, out string error)
        {
            error = null;
            return age >= 0 && age <= 150; // FIXME: Age must be 0-150
        }
    }
}
