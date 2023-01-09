using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
//using is same as import in Java

namespace Day01HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            
            Console.WriteLine("What is your name? ");
            string name = Console.ReadLine();

            Console.WriteLine("How old are you? ");
            string ageStr = Console.ReadLine();

            int age = int.Parse(ageStr); // ex!

            Console.WriteLine("Hello {0}, you are {1} y/o", name, age);
            Console.ReadKey();
        }
    }
}