using System;
using System.Runtime.CompilerServices;

namespace CoffeeMachine        
{
    // Type of coffee 
    class coffeePortion
    {
        public int coffee;
        public int sugar;
        public int milk;
        public int water;
        public int cup;
                  
        public coffeePortion(int _coffee, int _sugar, int _milk, int _water, int _cup)
        {
            coffee     = _coffee;
            cup        = _cup;
            milk       = _milk;
            sugar      = _sugar;
            water      = _water;
        }
    }

    // Coffee Machine 
    class Machine
    {
        // Initial amount of components
        private int _coffeeBeansAmount = 400;
        private int _sugarAmount = 300;
        private int _milkAmount = 100;
        private int _waterAmount = 2000;
        private int _cupsAmount = 3;
        private int _addcoffeeBeans = 0, _addSugar = 0, _addMilk = 0, _addWater = 0, _addCups = 0;

        public string[] supplies = new string[5] { "Coffe beans (g)", "Sugar (g)", "Milk (ml)", "Water (ml)", "Cups" };

        // getters
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

        // check amount of supplies and decrease them (if possible) by type of coffee
        public void makeCoffee(coffeePortion c)
        {
            // check that the amount of ingredients is enough to make a cup of coffee
            if (_cupsAmount >= c.cup && _sugarAmount >= c.sugar && _coffeeBeansAmount >= c.coffee && _waterAmount >= c.water && _milkAmount >= c.milk)
            {
                _cupsAmount         -= c.cup;
                _sugarAmount        -= c.sugar;
                _coffeeBeansAmount  -= c.coffee;
                _waterAmount        -= c.water;
                _milkAmount         -= c.milk;
            }
            else
            {
                throw new StackOverflowException("Not enough components");
            }
        }

        // check supplies to add and keep them to temporary storage  
        public bool addComponent(string name, string amountInput)
        {
            int amount;

            if (!int.TryParse(amountInput, out amount))
                return false;

            if (amount < 0)
                return false;

            switch (name)
            {
                case "Coffe beans (g)": 
                    _addcoffeeBeans = amount;
                    break; 
                case "Sugar (g)":
                    _addSugar = amount;
                    break;
                case "Cups":
                    _addCups = amount;
                    break;
                case "Milk (ml)":
                    _addMilk = amount;
                    break;
                case "Water (ml)":
                    _addWater = amount;
                    break;
            }

            return true;
        }

        // save all added supplies
        public void saveAdding()
        {
            _coffeeBeansAmount  += _addcoffeeBeans;
            _sugarAmount        += _addSugar;
            _milkAmount         += _addMilk;
            _waterAmount        += _addWater;
            _cupsAmount         += _addCups;
            resetAdding();
        }

        // reset temporary storage of supplies
        public void resetAdding()
        {
            _addcoffeeBeans = 0;
            _addSugar = 0;
            _addMilk = 0;
            _addWater = 0;
            _addCups = 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input;

            Machine M = new Machine();
            string[] supplies = M.supplies;
            coffeePortion capuccino = new coffeePortion(30, 10, 50, 50, 1);
            coffeePortion americano = new coffeePortion(40, 5, 0, 50, 1);
            
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
                    while (true)
                    {
                        Console.WriteLine("\n=====");
                        Console.WriteLine("What type of coffee do you prefer?");
                        Console.WriteLine("1. Americano");
                        Console.WriteLine("2. Capuccino");
                        Console.WriteLine("0. Back");

                        input = Console.ReadLine();

                        if (input == "0")
                        {
                            Console.WriteLine("Have a good day!");
                            break;
                        }
                        else if (input == "1" || input == "2")
                        {                            
                            try
                            {
                                M.makeCoffee((input == "1") ? americano : capuccino);
                                Console.WriteLine("Good choice, enjoy your coffee!\n");
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Impossible to do!\n");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong choice :) Try again ;)");
                        }
                    }
                }
                else if (input == "2")          // Service mode
                {
                    while (true)
                    {
                        Console.WriteLine("\n=====");
                        Console.WriteLine("Service mode:");
                        Console.WriteLine("1. Show remainings");
                        Console.WriteLine("2. Add supplies");
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
                        else if (input == "2")  // add supplies
                        {
                            int addedSupplies = 0;
                            Console.WriteLine("Please add supply:");

                            foreach (string s in supplies)
                            {
                                Console.WriteLine(s);
                                input = Console.ReadLine();
                                if (!M.addComponent(s, input))
                                {
                                    Console.WriteLine("Only positive or zero integers allowed for entering supplies\nAll entered values will be ignored");
                                    break;
                                }
                                addedSupplies++;
                            }

                            if (addedSupplies == supplies.Length)
                                M.saveAdding();
                            else
                                M.resetAdding();

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
