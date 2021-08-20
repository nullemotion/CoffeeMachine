using System;
using System.Runtime.CompilerServices;

namespace CoffeeMachine
{
    class Machine
    {
        // Initial amount of components
        private int _coffeeBeansAmount = 400;
        private int _sugarAmount = 300;
        private int _milkAmount = 100;
        private int _waterAmount = 2000;
        private int _cupsAmount = 3;

        public int coffeeBeansAmount
        {
            get => _coffeeBeansAmount;
        }

        public int sugarAmount
        {
            get => _sugarAmount;
        }

        public int milkAmount
        {
            get => _milkAmount;
        }

        public int waterAmount
        {
            get => _waterAmount;
        }

        public int cupsAmount
        {
            get => _cupsAmount;
        }

        public void makeCoffee()
        {
            // check that the amount of ingredients is enough to make a cup of coffee
            if (_cupsAmount > 0 && _sugarAmount >= 10 && _coffeeBeansAmount >= 30 && _waterAmount >= 50 && _milkAmount >= 10)
            {
                _cupsAmount -= 1;
                _sugarAmount -= 10;
                _coffeeBeansAmount -= 30;
                _waterAmount -= 50;
                _milkAmount -= 10;
            }
            else
            {
                throw new StackOverflowException("Not enough components");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input;

            Machine M = new Machine(); 
            
            while (true)
            {
                Console.WriteLine("=====");
                Console.WriteLine("Hello, how can I help you?");
                Console.WriteLine("1. Make coffee");
                Console.WriteLine("2. Service mode");
                Console.WriteLine("0. Exit");  

                input = Console.ReadLine(); 

                if (input == "0")               // exit
                {
                    Console.WriteLine("Have a good day!");
                    break;
                }
                else if (input == "1")          // make coffee
                {
                    try
                    {
                        M.makeCoffee();
                        Console.WriteLine("Good choice, enjoy your coffee!\n");
                    }
                    catch
                    {
                        Console.WriteLine("Impossible to do!\n");
                    }
                }
                else if (input == "2")          // Service mode
                {
                    while (true)
                    {
                        Console.WriteLine("\n=====");
                        Console.WriteLine("Service mode:");
                        Console.WriteLine("1. Show remainings");
                        Console.WriteLine("0. Back");

                        input = Console.ReadLine();

                        if (input == "0")       // Back
                        {
                            Console.WriteLine("");
                            break;
                        }
                        else if (input == "1")  // Show remainings
                        {
                            Console.WriteLine($"Coffe beans {M.coffeeBeansAmount} g");
                            Console.WriteLine($"Sugar {M.sugarAmount} g");
                            Console.WriteLine($"Milk {M.milkAmount} ml");
                            Console.WriteLine($"Water {M.waterAmount} ml");
                            Console.WriteLine($"{M.cupsAmount} cups");
                        }
                        else                    // wrong choice in service mode
                        {
                            Console.WriteLine("Wrong choice :) Try again ;)");
                        }
                    }
                }
                else                            // wrong choine in main menu
                {
                    Console.WriteLine("Wrong choice :) Try again ;)\n");          
                }     
            }            
        }
    }
}
