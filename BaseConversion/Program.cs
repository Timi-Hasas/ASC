using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseConversion
{
    class Program
    {
        static double ConvertToTen(string numar, int baza)
        {
            Stack<int> stiva = new Stack<int>();
            int numarCifre = 0;
            bool isInt = true;

            Console.WriteLine(numar);

            for(int i = numar.Length - 1; i >= 0; i--)
            {
                if (numar[i] == '.')
                {
                    isInt = false;
                    numarCifre = numar.Length - 1 - numarCifre;
                }

                if (isInt)
                    numarCifre++;

                if(numar[i]>='0' && numar[i]<='9')
                {
                    stiva.Push((int)Char.GetNumericValue(numar[i]));
                }
                else
                switch (numar[i])
                {
                    case 'A':
                        stiva.Push(10);
                        break;
                    case 'B':
                        stiva.Push(11);
                        break;
                    case 'C':
                        stiva.Push(12);
                        break;
                    case 'D':
                        stiva.Push(13);
                        break;
                    case 'E':
                        stiva.Push(14);
                        break;
                    case 'F':
                        stiva.Push(15);
                        break;
                }
            }

            double currentNumber = 0;
            double convertedNumber = 0;

            while (stiva.Count > 0)
            {
                currentNumber = stiva.Pop();
                convertedNumber = convertedNumber + (currentNumber * Math.Pow(baza, (numarCifre - 1)));
                numarCifre--;
            }

            return convertedNumber;
        }

        static string ConvertFromTen(double numar, int bazaTinta)
        {
            string result = "";

            int parteaIntreaga = (int)numar;
            double parteaFractionara = numar - (int)numar;

            int rest;
            Stack<int> stiva = new Stack<int>();

            while (parteaIntreaga > 0)
            {
                rest = parteaIntreaga % bazaTinta;
                stiva.Push(rest);
                parteaIntreaga = parteaIntreaga / bazaTinta;
            }

            while (stiva.Count > 0)
            {
                int numarCurent = stiva.Pop();
                if (numarCurent < 10)
                {
                    result = result + numarCurent;
                }
                else
                {
                    switch (numarCurent)
                    {
                        case 10:
                            result = result + "A";
                            break;
                        case 11:
                            result = result + "B";
                            break;
                        case 12:
                            result = result + "C";
                            break;
                        case 13:
                            result = result + "D";
                            break;
                        case 14:
                            result = result + "E";
                            break;
                        case 15:
                            result = result + "F";
                            break;
                    }
                }
            }

            if (parteaFractionara != 0)
            {
                result = result + '.';

                while (parteaFractionara != 0)
                {
                    result = result + (int)(parteaFractionara * bazaTinta);
                    parteaFractionara = parteaFractionara * bazaTinta - (int)(parteaFractionara * bazaTinta);
                }

                return result;
            }

            else

            return result;
        }


        static void Main(string[] args)
        {

            Console.WriteLine("****************************");
            Console.WriteLine("Base Conversion Application");
            Console.WriteLine("****************************");
            Console.WriteLine();

            bool isValid = true;
            string initialNumber;
            int initialBase;
            int convertedBase;

            try
            {
                do
                {
                    Console.Write("Scrieti numarul pe care doriti sa-l convertiti: ");
                    initialNumber = Console.ReadLine();
                    isValid = true;
                    for (int i = 0; i < initialNumber.Length; i++)
                    {
                        
                        if (((initialNumber[i] >= '0' && initialNumber[i] <= '9') || initialNumber[i] == 'A' || initialNumber[i] == 'B' || initialNumber[i] == 'C' || initialNumber[i] == 'D' || initialNumber[i] == 'E' || initialNumber[i] == 'F' || initialNumber[i] == '.') && isValid)
                        {            
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }

                    if(!isValid)
                        Console.WriteLine("Invalid Number!");

                } while (!isValid);
                
                do
                {
                    isValid = false;
                    Console.Write("Scrieti baza numarului pe care doriti sa-l convertiti: ");
                    initialBase = int.Parse(Console.ReadLine());
                    if (initialBase >= 2 && initialBase <= 16)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Baza trebuie sa fie intre 2 si 16!");
                        return;
                    }
                } while (!isValid);

                if(initialBase == 10)



                isValid = false;

                do
                {
                    Console.Write("Scrieti baza in care doriti sa convertiti numarul: ");
                    convertedBase = int.Parse(Console.ReadLine());
                    if (convertedBase >= 2 && convertedBase <= 16)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Baza trebuie sa fie intre 2 si 16!");
                    }
                } while (!isValid);

                if(initialBase == 10)
                {
                    double number = double.Parse(initialNumber);
                    string convertedNumber = ConvertFromTen(number, convertedBase);

                    Console.WriteLine("Numarul {0} scris in baza {1} convertit in baza {2} este egal cu: {3}", number, initialBase, convertedBase, convertedNumber);
                }
                else
                {
                    double number = ConvertToTen(initialNumber, initialBase);
                    string convertedNumber = ConvertFromTen(number, convertedBase);
                    Console.WriteLine("Numarul {0} scris in baza {1} convertit in baza {2} este egal cu: {3}", initialNumber, initialBase, convertedBase, convertedNumber);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Numbers!");
            }
        }
    }
}
                