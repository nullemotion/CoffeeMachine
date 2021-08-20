using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    public class Ingredient
    {
        //Fields
        private string _measure;
        private int _amount;
        private int _amountAdded = 0;
        private string _longName;

        // Constructor
        public Ingredient(string LongName, string Measure, int Amount)
        {
            _longName = LongName;
            _measure = Measure;
            _amount = Amount;

        }

        // Propterties
        public string Measure
        {
            get => _measure;
        }

        public int Amount
        {
            get => _amount;
        }

        public string LongName
        {
            get => _longName;
        }

        // Methods

        // Add some measure of ingredient to temporary storage
        public void Add(int measure)
        {
            _amountAdded = measure;
        }

        // Save added ingredient
        public void SaveAdd()
        {
            _amount += _amountAdded;
            _amountAdded = 0;
        }

        // Reset temporary storage
        public void reset()
        {
            _amountAdded = 0;
        }
    }
}
