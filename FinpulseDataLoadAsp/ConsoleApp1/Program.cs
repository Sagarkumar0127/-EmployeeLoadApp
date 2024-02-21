using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(DateTime.Now);
            string day = DateTime.Now.DayOfWeek.ToString();
            Console.WriteLine(DateTime.Now.DayOfWeek.ToString());
            Console.WriteLine(day);
            Console.WriteLine(day.GetType());
            Console.ReadLine();


        }
    }
}
