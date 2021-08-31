using System;

namespace atividade1
{
    class Program
    {
        // Fazer um algoritmo que imprima os numeros de 2 até 100
        // para cada número, imprima os seus antecessores pares
        public static void Main(string[] args)
        {
            int numero = 2;

            while(numero <= 100)
            {   
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Antecessores de {numero}: ");
                Console.ResetColor();

                int i = 0;
                while(i < numero)
                {
                    if(i % 2 == 0)
                    {
                        Console.Write(i + " ");
                    }

                    i++;
                }
                Console.WriteLine("\n");
                numero++;
            }
        }
    }
}
