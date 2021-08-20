using System;
using System.Runtime.CompilerServices;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;         
            
            while (true)
            {
                Console.WriteLine("=====");
                Console.WriteLine("Hello, how can I help you?");
                Console.WriteLine("1. Make coffee");
                Console.WriteLine("0. Exit");  

                input = Console.ReadLine(); 

                if (input == "0")
                {
                    Console.WriteLine("Have a good day!");
                    break;
                }
                else if (input == "1")
                {
                    Console.WriteLine("Good choice, enjoy your coffee!");
                    Console.WriteLine("");                    
                }
                else
                {
                    Console.WriteLine("Wrong choice :) Try again ;)");
                    Console.WriteLine("");                    
                }     
            }            
        }
    }
}
