using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    class Machine
    {
        public char InsertCoin()
        {
            char coin = '0';
            bool isValid = false; ;
            char[] coins = { 'n', 'N', 'd', 'D', 'q', 'Q' };
            do
            {
                try
                {
                    coin = char.Parse(Console.ReadLine());
                    if (coins.Contains(coin))
                        isValid = true;
                    if (!isValid)
                        Console.WriteLine("Invalid");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid");
                }
            } while (!isValid);
            return coin;
        }

        private char state;
        public char GetState()
        {
            return state;
        }

        public void SetState(char state)
        {
            this.state = state;
        }

        public void ReturnNickel()
        {
            Console.Write("1 Nickel ");
        }
        public void ReturnDime()
        {
            Console.Write("1 Dime ");
        }
        public void Dispense()
        {
            Console.WriteLine();
            Console.WriteLine("******************");
            Console.WriteLine("Here's your coffee!");
            Console.WriteLine("Enjoy your drink!");
            Console.WriteLine("*******************");
            Console.WriteLine();
        }
   
        
    }
}
