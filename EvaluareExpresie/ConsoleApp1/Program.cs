using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static float Calculate(string expression)
        {
            Stack<float> result = new Stack<float>();        // Cream o stiva "result" pentru evaluarea expresiei
            List<string> exp = RPN(expression);              // Cream o lista "exp" in care vom pune RPN-ul expresiei
            float number1, number2;                           
            foreach(string token in exp)                     //parcurgem fiecare numar sau operator din lista 
            {
                if (token[0] >= '0' && token[0] <= '9')      //daca token-ul din lista este numar
                    result.Push(float.Parse(token));         //atunci punem numarul in stiva
                else
                {                                                  //in caz contrar, daca token-ul este operator atunci
                    number1 = result.Pop();                        //scoatem ultimele doua valori din stiva dupa care
                    number2 = result.Pop();                        //adaugam rezultatul operatiei dintre ultimul si penultimul
                    result.Push(ToAdd(number2, token, number1));   //numar scos din stiva, conform operatorului
                }
            }
            return result.Pop();    //returnam elementul ramas in stiva, care este rezultatul expresiei
        }
        static float ToAdd(float n2, string op, float n1)
        {
            float result = 0;
            switch(op)
            {
                case "+": result = n2 + n1;
                    break;
                case "-": result = n2 - n1;
                    break;
                case "*": result = n2 * n1;
                    break;
                case "/": result = n2 / n1;
                    break;
            }
            return result;
        }
        static List<string> RPN(string expression)
        {
            string[] expTokens = Tokens(expression);      //Impartim expresia data in subsiruri, fiecare numar, operator, paranteza reprezentand un subsir
            Stack<string> stack = new Stack<string>();    //Cream o stiva de care ne vom folosi in conversia expresiei
            List<string> result = new List<string>();     //Cream o lista care va deveni expresia convertita in format RPN
            string[] operators = { "+", "-", "*", "/" };  
            for (int i = 0; i < expTokens.Length; i++)        //Parcurgem fiecare subsir(token) al expresiei
            {
                if (expTokens[i][0] >= '0' && expTokens[i][0] <= '9')    //Verificam daca subsirul curent este numar
                    result.Add(expTokens[i]);                            //si il adaugam in lista 'result' daca este adevarat
                else
                    if (expTokens[i] == "(")       //in caz contrar, verificam daca subsirul este '(' (inceputul unei serii de calcule in paranteza)
                    stack.Push(expTokens[i]);      //si adaugam in stiva token-ul daca conditia este indeplinita
                else
                    if (expTokens[i] == ")")       //in caz contrar, verificam daca token-ul est egal cu ')' (sfarsitul unei serii de calcule in paranteza)
                {                                                         
                    while (stack.Count() != 0 && stack.Peek() != "(")    //daca conditia este indeplinita,  
                        result.Add(stack.Pop());                         //punem toti operatorii din stiva in 
                    stack.Pop();                                         //lista 'result' si eliminam '(' curent
                }
                else
                if(operators.Contains(expTokens[i]))                                                 //Verificam daca subsirul este operator,
                {                                                                                    //iar daca conditia este indeplinita
                    while (stack.Count() != 0 && Priority(stack.Peek()) >= Priority(expTokens[i]))   //parcurgem stiva si punem in lista operatorii
                        result.Add(stack.Pop());                                                     //de prioritate mai mare sau egala cu operatorul curent
                    stack.Push(expTokens[i]);                                                        //dupa care adaugam operatorul curent in stiva
                }
            }
            while (stack.Count != 0)        //Parcugem stiva si punem operatorii ramasi in lista
                result.Add(stack.Pop());
            return result;
        }
        static int Priority(string op)
        {
            if (op == "*" || op == "/")
                return 2;
            if (op == "+" || op == "-")
                return 1;

            return 0;
        }
        static string[] Tokens(string exp)
        {
            string spaces = "";
            for (int i = 0; i < exp.Length - 1; i++)
                if (exp[i] >= '0' && exp[i] <= '9' && exp[i + 1] >= '0' && exp[i] <= '9')
                    spaces += exp[i];
                else
                    spaces += exp[i] + " ";

            spaces += exp[exp.Length - 1];

            string[] tokens = spaces.Split(' ');
            return tokens;
        }
        static string Read()
        {
            string expression = "";
            bool isValid;
            do
            {
                try
                {
                    expression = Console.ReadLine();
                    isValid = Validate(expression);
                    if(!isValid)
                        Console.WriteLine("Invalid expression");
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid expression");
                    isValid = false;
                } 
            } while (!isValid);
            return expression;
        }
        static bool Validate(string exp)
        {
            int bracket = 0;
            char[] op = { '+', '-', '*', '/' };
            bool containsOp = false, containsNumber = false;

            if (op.Contains(exp[0]) || op.Contains(exp[exp.Length - 1]))  //Verificam daca expresia se termina sau nu cu un operator
                return false;
            
            for (int i = 0; i < exp.Length; i++)
            {
                if (exp[i] >= '0' && exp[i] <= '9' && !containsNumber)   //Ne asiguram ca expresia contine numere
                    containsNumber = true;
                if (op.Contains(exp[i]) && !containsOp)   //Ne asiguram ca expresia contine operatori
                    containsOp = true;

                if (exp[i] == '(')    //Verificam daca parantezele sunt puse corect in expresie
                    bracket++;
                else
                if (exp[i] == ')')
                    bracket--;
                else
                if ((exp[i] < '0' || exp[i] > '9') && (!op.Contains(exp[i])))  //Verificam daca expresia nu mai contine si alte caractere
                    return false;
                if (bracket < 0)    //In cazul in care numarul parantezelor ajunge la un moment dat sa fie negativ, expresia este incorecta
                    return false;              
                if(i != 0)
                {    
                    if (op.Contains(exp[i]) && (op.Contains(exp[i - 1]) || exp[i-1] == '('))   //Verificam daca operatorii sunt pozitionati corect in expresie
                        return false;
                    if (exp[i] == ')' && op.Contains(exp[i - 1]))
                        return false;
                }
            }
            if (bracket != 0 || !containsOp || !containsNumber)    //Daca expresia contine doar numere, doar operatori 
                return false;                                      //sau numarul final parantezelor nu este 0
                                                                   //expresia este incorecta
            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Write an expression: ");
            string exp = Read();
            List<string> rpn = new List<string>();
            rpn = RPN(exp);
            foreach(string token in rpn)
                Console.Write($"{token} ");
            Console.WriteLine();
            Console.WriteLine($"Result = {Calculate(exp)}");
        }
    }
}
