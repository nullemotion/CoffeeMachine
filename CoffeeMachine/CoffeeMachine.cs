using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    // Coffee Machine 
    public class CoffeeMachine
    {
        // Fields
        private Dictionary<string, CoffeePortion> _recipes = new Dictionary<string, CoffeePortion>();
        private Dictionary<string, Ingredient> _ingredients = new Dictionary<string, Ingredient>();
        private Dictionary<string, CoffeePortion> _coffeeMenu = new Dictionary<string, CoffeePortion>();

        // Constructor
        public CoffeeMachine()                                
        {
            _recipes.Add("Americano", new CoffeePortion(new string[] { "coffee", "40", "sugar", "5", "milk", "0", "water", "50", "cups", "1" }));
            _recipes.Add("Capuccino", new CoffeePortion(new string[] { "coffee", "30", "sugar", "10", "milk", "50", "water", "50", "cups", "1" }));

            _ingredients.Add("coffee",  new Ingredient("Coffee beans", "g", 400));
            _ingredients.Add("sugar",   new Ingredient("Sugar", "g", 300));
            _ingredients.Add("milk",    new Ingredient("Milk", "ml", 100));
            _ingredients.Add("water",   new Ingredient("Water", "ml", 2000));
            _ingredients.Add("cups",    new Ingredient("Cups", "pcs", 3));

            Init();
        }

        //// Methods 

        // Initialize numbers in menu
        private void Init()                              
        {
            int counter = 1;
            _coffeeMenu.Clear();
            foreach (string name in _recipes.Keys)
            {
                _coffeeMenu.Add(counter.ToString(), _recipes[name]);
                counter++;                
            }
        }

        // Main menu
        private void ShowMainMenu()                      
        {
            Console.WriteLine("=====\nHello, how can I help you?");
            Console.WriteLine("1. Make coffee");
            Console.WriteLine("2. Service mode");
            Console.WriteLine("0. Exit");
        }

        // Make coffee menu
        private void ShowCoffeeTypes()                   
        {
            int counter = 1;
            Console.WriteLine("\n=====\nWhat type of coffee do you prefer?");
            foreach (string name in _recipes.Keys)
            {
                Console.WriteLine($"{counter}. {name}");
                counter++;
            }
            Console.WriteLine("0. Back");
        }

        // remainings supply
        private void ShowRemainings()                    
        {
            foreach (string name in _ingredients.Keys)
            {
                Console.WriteLine($"{_ingredients[name].LongName} {_ingredients[name].Amount} {_ingredients[name].Measure}");
            }
        }

        // Service mode
        private void ShowServiceMenu()                   
        {
            Console.WriteLine("\n=====\nService mode:");
            Console.WriteLine("1. Show remainings");
            Console.WriteLine("2. Add supplies");
            Console.WriteLine("3. Add recipe");
            Console.WriteLine("4. Add ingredient");
            Console.WriteLine("0. Back");
        }

        // Add supply process
        private bool AddSupplies()                       
        {
            Console.WriteLine("Please add supply:");
            foreach (string name in _ingredients.Keys)
            {
                Console.WriteLine($"{_ingredients[name].LongName} ({_ingredients[name].Measure})");
                string input = Console.ReadLine();
                int amount;
                if (int.TryParse(input, out amount) && amount >= 0)
                {
                    _ingredients[name].Add(amount);
                }
                else
                {
                    Console.WriteLine("Only positive or zero integers allowed for entering supplies\nAll entered values will be ignored");
                    return false;
                }
            }
            foreach (string name in _ingredients.Keys)
            {
                _ingredients[name].SaveAdd();
            }
            return true;
        }

        // Reset "add suply" process
        private void ResetAdding()                           
        {
            foreach (string name in _ingredients.Keys)
            {
                _ingredients[name].reset();
            }
        }

        // Add new recipe
        private void AddRecipe()                             
        {
            string input, coffeeName;
            List<string> recipe = new List<string>();
            int amount;

            Console.WriteLine("Please enter the name of the coffee product:");
            coffeeName = Console.ReadLine();

            foreach (string name in _ingredients.Keys)
            {
                Console.WriteLine($"{_ingredients[name].LongName} ({_ingredients[name].Measure})");
                input = Console.ReadLine();
                if (int.TryParse(input, out amount) && amount >= 0)
                {
                    recipe.Add(name);
                    recipe.Add(input);
                }
                else
                {
                    Console.WriteLine("Only positive or zero integers allowed for entering supplies\nAll entered values will be ignored");
                    return;
                }
            }

            _recipes.Add(coffeeName, new CoffeePortion(recipe.ToArray()));
            Init();
        }

        // Add new ingredient
        private void AddIngredient()                         
        {
            string name, measure;
            Console.WriteLine("Please add new ingredient:");
            Console.WriteLine("Ingredient name:");
            name = Console.ReadLine();
            Console.WriteLine("Measure:");
            measure = Console.ReadLine();

            _ingredients.Add(name, new Ingredient(name, measure, 0));
        }

        // Compare available supplies with ingredients needed per cup of coffee
        private bool CheckSuppliesIsEnough(string number)  
        {
            CoffeePortion c = _coffeeMenu[number];
            foreach (string name in c.GetAllIngredients())
            {
                if (_ingredients[name].Amount < c.GetIngredient(name))
                    return false;
            }
            return true;
        }

        // Decrease amount of supplies per cup of coffee
        private void MakeCoffee(string number)             
        {
            if(CheckSuppliesIsEnough(number))
            {
                CoffeePortion cupOfCoffee = _coffeeMenu[number];
                foreach (string name in cupOfCoffee.GetAllIngredients())
                {
                    _ingredients[name].Add(-cupOfCoffee.GetIngredient(name));
                    _ingredients[name].SaveAdd();
                }
                Console.WriteLine("Good choice, enjoy your coffee!\n");
            }
            else
            {
                Console.WriteLine("Impossible to do!\n");
            }
            return;
        }

        // Run
        public void Run()
        {
            string input;

            while (true)
            {
                ShowMainMenu();

                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.WriteLine("Have a good day!");
                        return;
                    case "1":
                        MakeCoffeeMenu();
                        break;
                    case "2":
                        ServiceMode();
                        break; 
                    default:
                        Console.WriteLine("Wrong choice :) Try again ;)\n");
                        break;
                }  
            }
        }

        // "1. Make coffee" menu
        private void MakeCoffeeMenu()
        {
            string input;            

            while(true)
            {
                ShowCoffeeTypes();

                input = Console.ReadLine();
                switch (input)
                {
                    case string coffeeNumber when _coffeeMenu.Keys.Contains(coffeeNumber):
                        MakeCoffee(coffeeNumber);
                        return;
                    case "0":
                        Console.WriteLine("");
                        return;
                    default:
                        Console.WriteLine("Wrong choice :) Try again ;)");
                        break;
                }
            }
        }

        // "2. Service mode" menu
        private void ServiceMode()
        {
            string input;            

            while (true)
            {
                ShowServiceMenu();

                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.WriteLine("");
                        return;
                    case "1":
                        ShowRemainings();
                        break;
                    case "2":
                        AddSupplies();
                        break;
                    case "3":
                        AddRecipe();
                        break;
                    case "4":
                        AddIngredient();
                        break;
                    default:
                        Console.WriteLine("Wrong choice :) Try again ;)");
                        break;
                }
            }
        }
    }
}