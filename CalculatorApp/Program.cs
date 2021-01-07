using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    class Program
    {
        static void CifreVector(string numar, ref int[] vectorCifre)
        {
            vectorCifre = new int[numar.Length];
            int nrCifre = vectorCifre.Length;
            for (int i = 0; i < nrCifre; i++)
                vectorCifre[i] = (int)char.GetNumericValue(numar[nrCifre - i - 1]);
        }
        static int VectorCifre(int[] vectorCifre)
        {
            int numar = 0;
            for (int i = vectorCifre.Length - 1; i >= 0; i--)
                numar = numar * 10 + vectorCifre[i];
            return numar;
        }
        static void AfisareRezultat(int[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n / 2; i++)
            {
                int aux = a[i];
                a[i] = a[n - i - 1];
                a[n - i - 1] = aux;
            }
            for (int i = 0; i < n; i++)
                Console.Write($"{a[i]}");
            Console.WriteLine();
        }
        static void Suma(int[] number1, int[] number2, ref int[] result)
        {
            int n = number1.Length;
            int m = number2.Length;
            int min, max;
            max = Math.Max(n, m);
            min = Math.Min(n, m);
            int transport = 0;
            int[] temp = new int[max];
            for (int i = 0; i < min; i++)
            {
                int sum = number1[i] + number2[i] + transport;
                temp[i] = sum % 10;
                if (sum > 9)
                    transport = 1;
                else
                    transport = 0;
            }
            if (n == max)
                for (int i = min; i < max; i++)
                {
                    int sum = number1[i] + transport;
                    temp[i] = sum % 10;
                    if (sum > 9)
                        transport = 1;
                    else
                        transport = 0;
                }
            else
            if (m == max)
                for (int i = min; i < max; i++)
                {
                    int sum = number2[i] + transport;
                    temp[i] = sum % 10;
                    if (sum > 9)
                        transport = 1;
                    else
                        transport = 0;
                }
            if (transport == 1)
            {
                result = new int[max + 1];
                int i;
                for (i = 0; i < max; i++)
                    result[i] = temp[i];
                result[i] = transport;
            }
            else
                result = temp;
        }
        static void Produs(int[] number1, int[] number2, ref int[] result)
        {
            int n = number1.Length;
            int m = number2.Length;
            int min, max;
            max = Math.Max(n, m);
            min = Math.Min(n, m);

            int[,] temp = new int[max, max + min];
            int produs;
            int k = 0, p;
            int transport = 0;

            for (int i = 0; i < n; i++)
            {
                p = 0;
                for (int idx = 0; idx < i; idx++)
                {
                    temp[k, p] = 0;
                    p++;
                }
                for (int j = 0; j < m; j++)
                {
                    produs = number1[i] * number2[j] + transport;
                    temp[k, p] = produs % 10;
                    if (produs > 9)
                        transport = produs / 10;
                    else
                        transport = 0;
                    p++;
                }
                if (transport != 0)
                    temp[k, p] = transport;
                transport = 0;
                k++;
            }
            result = new int[temp.GetLength(1)];
            int[] suma = new int[temp.GetLength(1)];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1); j++)
                    suma[j] = temp[i, j];
                Suma(suma, result, ref result);
            }
            if (result.Last() == 0)
            {
                int[] tempRes = result;
                result = new int[tempRes.Length - 1];
                for (int i = 0; i < result.Length; i++)
                    result[i] = tempRes[i];
            }
        }
        static void Putere(int[] number1, int[] number2, ref int[] result)
        {
            long n = VectorCifre(number2);
            if (n == 0)
            {
                result = new int[1];
                result[0] = 1;
            }
            else
            if (n == 1)
                result = number1;
            else
            {
                Produs(number1, number1, ref result);
                for (int i = 2; i < n; i++)
                    Produs(number1, result, ref result);
            }
        }
        static void Diferenta(int[] number1, int[] number2, ref int[] result)
        {

            int n = number1.Length;
            int m = number2.Length;
            int min, max;
            max = Math.Max(n, m);
            min = Math.Min(n, m);
            int[] temp = new int[max];
            for (int i = 0; i < min; i++)
            {
                if (number2[i] > number1[i])
                {
                    int k = i;
                    bool stop = false;
                    do
                    {
                        k++;
                        if (number1[k] == 0)
                            number1[k] = 9;
                        else
                            stop = true;
                    }
                    while (!stop);

                    number1[k] = number1[k] - 1;
                    number1[i] = 10 + number1[i];
                    temp[i] = number1[i] - number2[i];
                }
                else
                    temp[i] = number1[i] - number2[i];           
            }
            for (int i = min; i < max; i++)
                temp[i] = number1[i];

            int nrZero = 0;
            int j = 0;
            while (temp[max - 1 - j] == 0 && j < max - 1)
            {
                nrZero++;
                j++;
            }           
            if(nrZero != temp.Length)
            result = new int[temp.Length - nrZero];
            for (int i = 0; i < result.Length; i++)
                result[i] = temp[i];

        }
        static void Impartire(int[] number1, int[] number2, ref int[] result)
        {
            Console.WriteLine("Work in progress!");
        }
        static string ReadNumber()
        {
            string n = "";
            bool isValid; ;
            do
            {
                try
                {
                    isValid = true;
                    n = Console.ReadLine();
                    for (int i = 0; i < n.Length; i++)
                        if (n[i] < '0' || n[i] > '9' || (i == 0 && n[i] == '0' && n.Length != 1))
                            isValid = false;
                    if (!isValid)
                        Console.WriteLine("Invalid");
                }
                catch (Exception)
                {
                    isValid = false;
                    Console.WriteLine("Invalid");
                }
            } while (!isValid);
            return n;
        }
        static char Operator()
        {
            bool isValid = false;
            char op = ' ';
            char[] operators = { '+', '-', '*', '/', '^' };
            do
            {
                try
                {
                    op = char.Parse(Console.ReadLine());
                    if (operators.Contains(op))
                        isValid = true;
                    if (!isValid)
                        Console.WriteLine("Invalid");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid");
                    isValid = false;
                }
            } while (!isValid);
            return op;
        }
        static bool Compara(int[] a, int[] b)
        {
            int n = a.Length;
            int m = b.Length;
            if (n > m)
                return true;
            if (m > n)
                return false;

            for (int i = n - 1; i >= 0; i--)
                if (a[i] >= b[i])
                    return true;
                else
                    return false;

            return true;
        }



        //Inmultirea cu scalar ( laborator FP)
        /*
        static int[] Scalar(int[] number1, int scalar)
        {
            int[] result;
            int[] temp = new int[number1.Length];
            int transport = 0;
            int produs;
            for(int i = 0; i < temp.Length; i++)
            {
                produs = number1[i] * scalar + transport;
                temp[i] = produs % 10;
                if (produs > 9)
                    transport = produs / 10;
                else
                    transport = 0;
            }
            if(transport != 0)
            {
                result = new int[temp.Length + 1];
                int i;
                for (i = 0; i < temp.Length; i++)
                    result[i] = temp[i];
                result[i] = transport;
            }
            else
                result = temp;
            

            return result;
        }
        static int[] Zero(int[]v, int nrZero)
        {
            int[] rez = new int[v.Length + nrZero];
            for (int i = rez.Length - 1; i >= nrZero; i--)
                rez[i] = v[i - nrZero];

            return rez;
        }
        static void InmultireScalar(int[]number1, int[]number2, ref int[] result)
        {
            for(int i = 0; i < number2.Length; i++)
            {
                Suma(result, Zero(Scalar(number1, number2[i]), number2.Length - 1 - i), ref result);
            }
        }
         */



        static void Main(string[] args)
        {
            string firstNumber, secondNumber;
            char op;

            Console.Write("Alegeti primul numar: ");
            firstNumber = ReadNumber();
            int[] number1 = { };
            CifreVector(firstNumber, ref number1);

            Console.Write("Alegeti operatia dorita: ");
            op = Operator();

            Console.Write("Alegeti al doilea numar: ");
            secondNumber = ReadNumber();
            int[] number2 = { };
            CifreVector(secondNumber, ref number2);
            int[] result = { };

            Console.Write("Rezultatul este: ");
            switch (op)
            {
                case '+':
                    Suma(number1, number2, ref result);
                    break;
                case '-':
                    bool semn = Compara(number1, number2);
                    if (semn)
                        Diferenta(number1, number2, ref result);
                    else
                    {
                        Diferenta(number2, number1, ref result);
                        Console.Write("-");
                    }
                    break;
                case '*':
                    Produs(number1, number2, ref result);                
                    break;
                case '/':
                    Impartire(number1, number2, ref result);
                    break;
                case '^':
                    Putere(number1, number2, ref result);
                    break;
                default:
                    Console.WriteLine("Invalid operator");
                    break;
            }

            AfisareRezultat(result);
            Console.WriteLine();
        }
    }
}
