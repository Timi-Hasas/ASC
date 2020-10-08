using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegeaLuiMoore
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.Write("Amplificarea puterii: ");
            n = int.Parse(Console.ReadLine());

            double rezultatLuni;
            rezultatLuni = (Math.Log(n, 2) * 18);

            double rezultatAni = rezultatLuni / 12;

            Console.WriteLine("In aproximativ " + Math.Round(rezultatAni) + " ani " + "(" + Math.Round(rezultatLuni) + " luni)" + " puterea va fi de " + n + " ori mai mare");
            Console.WriteLine("[" + rezultatAni + " ani " + "(" + rezultatLuni + " luni)]");
            Console.ReadKey();

            /* 
            Conform legii lui Moore (puterea de calcul se dublează la fiecare 18 luni iar prețul rămâne același),
            putem observa ca puterea de calcul creste exponential in baza 2 la fiecare 18 luni, astfel
            obtinem formula Log in baza 2 din n inmultit cu 18 pentru a obtine lunile necesare cresterii de n ori a
            puterii de calcul.
            
            Operatiile care implica numere cu virgula se executa de 100 de ori mai repede in hardware
            decat in software. Conform acestei afirmatii si a formulei de mai sus, atribuim lui n valorea 100, iar
            in urma calculului vom obtine faptul ca operatiile ce implica numere cu virgula au nevoie de aproximativ
            10 (9,96) ani pentru a se efectua la fel de repede ca si varianta hardware.
             */
        }
    }
}
