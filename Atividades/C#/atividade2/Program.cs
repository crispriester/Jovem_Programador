using System;

namespace atividade2
{
    class Program
    {
        // Fazer um algoritmo que imprima todos os números primos de 2 a 100
        public static void Main(string[] args)
        {
            int numero = 2;
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Números primos de 2 a 100:");
            Console.ResetColor();

            while(numero <= 100)
            {
                int qntdDivisores = 0;
                int divisor = 1;

                while(divisor <= numero)
                {
                    if(numero % divisor == 0)
                    {
                        qntdDivisores++;
                    }
                    
                    if(qntdDivisores > 2)
                    {
                        break;
                    }
        
                    divisor++;
                }

                if(qntdDivisores == 2)
                {
                    Console.Write(numero + " ");
                }

                numero++;
            }
        }   
    }
}
