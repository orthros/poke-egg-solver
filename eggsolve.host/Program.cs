using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using eggsolve.service;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using(ServiceHost sh = new ServiceHost(typeof(EggSolvingService)))
            {
                    sh.Open();
                    Console.WriteLine("Service are opened... waiting");
                    Console.ReadKey();
                    Console.WriteLine("Closing Services");
                    sh.Close();
            }
        }
    }
}
