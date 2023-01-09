using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01TextFile
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Write("What is your name? ");
            string name = Console.ReadLine();

            string path = @"C:\Users\Administrator\source\repos\Jingyu-An\2023-JohnAbbott-AppDev1\Day01TextFile\data.txt";

            try
            {
                FileStream fs = new FileStream(path, FileMode.Append);

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(fs);

                //Write a line of text
                sw.WriteLine(name);
                sw.WriteLine(name);
                sw.WriteLine(name);
                //Write a second line of text
                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(path);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
