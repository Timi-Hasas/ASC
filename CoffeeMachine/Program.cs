using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    class Program
    {
        static Machine machine = new Machine();
        static void StateA(char coin)
        {
            if (coin == 'N' || coin == 'n')
            {
                machine.SetState('B');
            }
            else
                if (coin == 'd' || coin == 'd')
            {
                machine.SetState('C');
            }
            else
                if (coin == 'Q' || coin == 'q')
            {
                machine.SetState('A');
                Console.Write("Your change: ");
                machine.ReturnNickel();
                machine.Dispense();
            }
        }

        static void StateB(char coin)
        {
            if (coin == 'N' || coin == 'n')
            {
                machine.SetState('C');
            }
            else
                if (coin == 'd' || coin == 'd')
            {
                machine.SetState('D');
            }
            else
                if (coin == 'Q' || coin == 'q')
            {
                machine.SetState('A');
                Console.Write("Your change: ");
                machine.ReturnDime();
                machine.Dispense();
            }
        }

        static void StateC(char coin)
        {
            if (coin == 'N' || coin == 'n')
            {
                machine.SetState('D');
            }
            else
                if (coin == 'd' || coin == 'd')
            {
                machine.SetState('A');
                machine.Dispense();
            }
            else
                if (coin == 'Q' || coin == 'q')
            {
                machine.SetState('A');
                Console.Write("Your change: ");
                machine.ReturnDime();
                Console.Write("+ ");
                machine.ReturnNickel();
                machine.Dispense();
            }
        }

        static void StateD(char coin)
        {
            if (coin == 'N' || coin == 'n')
            {
                machine.SetState('A');
                machine.Dispense();
            }
            else
                if (coin == 'd' || coin == 'd')
            {
                machine.SetState('A');
                Console.Write("Your change: ");
                machine.ReturnNickel();
                machine.Dispense();
            }
            else
                if (coin == 'Q' || coin == 'q')
            {
                machine.SetState('B');
                Console.Write("Your change: ");
                machine.ReturnDime();
                Console.Write("+ ");
                machine.ReturnNickel();
                machine.Dispense();
            }
        }
        static void Main(string[] args)
        {
            machine.SetState('A');
            Console.WriteLine("This machine accepts the following coins: ");
            Console.WriteLine("- Nickels (n or N)");
            Console.WriteLine("- Dimes (d or D)");
            Console.WriteLine("- Quarters (q or Q)");
            Console.WriteLine();
            do
            {
                Console.WriteLine($"Machine state: {machine.GetState()}");
                Console.Write("Insert your coin: ");
                char coin = machine.InsertCoin();
                Console.WriteLine();

                if (machine.GetState() == 'A')
                    StateA(coin);
                else
                if (machine.GetState() == 'B')
                    StateB(coin);
                else
                if (machine.GetState() == 'C')
                    StateC(coin);
                else
                if (machine.GetState() == 'D')
                    StateD(coin);

            } while (true);


        }
    }
}
