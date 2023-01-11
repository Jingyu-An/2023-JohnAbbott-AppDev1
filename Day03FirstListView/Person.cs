﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03FirstListView
{
    internal class Person
    {
        public string Name;
        public string Age;

        public Person(string name, string age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Name} is {Age} y/o (from ToString)";
        }
    }
}
