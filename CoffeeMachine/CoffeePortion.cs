using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class CoffeePortion
    {
        // Fields
        private Dictionary<string, int> _amounts = new Dictionary<string, int>();
        private List<string> _ingredients = new List<string>();

        // Constructor
        public CoffeePortion(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                int amount;
                int.TryParse(args[i + 1], out amount);
                _amounts.Add(args[i], amount);
                _ingredients.Add(args[i]);
            }
        }

        // Methods

        // Get ingredient by name
        public int GetIngredient(string name)
        {
            return _amounts[name];
        }

        // Get array of available ingredients
        public string[] GetAllIngredients()
        {
            return _ingredients.ToArray();
        }
    }
}
