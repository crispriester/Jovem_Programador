using System;

namespace avaliacao1
{
    class Program
    {
        public static void Main(string[] args)
        {
            StartProgram();
        }

        public static void StartProgram()
        {
            
            Console.Write("Qual é a data de sua viagem? ");
            DateTime travelDate = DateTime.Parse(Console.ReadLine());

            if(travelDate < DateTime.Today)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nPaulo, não será possível você viajar nesta data.");
                Console.ResetColor();
            }
            else
            {
                Console.Write("Qual é o peso de sua bagagem? ");
                decimal baggageWeight = decimal.Parse(Console.ReadLine());

                Console.Write("Como será a forma de pagamento do combustível? (vista, prazo) ");
                string formOfPayement = Console.ReadLine();

                decimal moneyPercentage = 0m;


                if(formOfPayement == "vista")
                {
                    Console.WriteLine("\nO tipo de pagamento será em: ");

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" - Para Dinheiro, escreva 'D'; \n - Para Cartão de Crédito, escreva 'C'; \n - Para Ambos, escreva 'A'.");
                    Console.ResetColor();

                    Console.Write(" Sua resposta: ");
                    char typeOfPayement = char.Parse(Console.ReadLine());

                    if(typeOfPayement == 'A')
                    {                        
                        Console.Write("Qual será o percentual que pagarás em dinheiro? ");
                        moneyPercentage = decimal.Parse(Console.ReadLine());

                        if((moneyPercentage > 90m) || (moneyPercentage < 10))
                        {   
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\nPaulo, é impossível continuar com o pagamento.");
                            Console.ResetColor();
                            return; 
                        }
                    }
                }

                decimal totalExpanseCarA = CalculateTotalExpense(travelDate, 'A', 45m, baggageWeight, formOfPayement, moneyPercentage);
                decimal totalExpanseCarB = CalculateTotalExpense(travelDate, 'B', 55m-8m, baggageWeight, formOfPayement, moneyPercentage);

                decimal leftoverLitersCarA = CalculateLeftoverLiters(326m, 12.8m, 45m);
                decimal leftoverLitersCarB = CalculateLeftoverLiters(326m, 14.1m, 55m);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nCarro: A; Gasto Total: R${totalExpanseCarA}; Sobra de litros no tanque: {leftoverLitersCarA} litros.");
                Console.WriteLine($"\nCarro: B; Gasto Total: R${totalExpanseCarB}; Sobra de litros no tanque: {leftoverLitersCarB} litros.");
                Console.ResetColor();
            }            

        }

        // Calcula os litros que restarão no carro
        static decimal CalculateLeftoverLiters(decimal distance, decimal kmPerLiter, decimal litersInTheTank)
        {
            decimal requiredLiters = distance / kmPerLiter;
            decimal leftoverLiters = litersInTheTank - requiredLiters;

            return decimal.Round(leftoverLiters, 2);
        }

        // Calcula o total a pagar
        static decimal CalculateTotalExpense(DateTime travelDate, char car, decimal litersToFill, decimal baggageWeight, string formOfPayement, decimal moneyPercentage)
        {
            decimal totalFuel = CalculateFuel(car, travelDate, litersToFill);
            decimal totalToll = CalculateTotalToll(travelDate, car, baggageWeight);
            decimal totalExpense;

            if (formOfPayement == "prazo")
            {
                totalFuel += 2.25m;

                if ((travelDate.DayOfWeek == DayOfWeek.Friday)   || 
                    (travelDate.DayOfWeek == DayOfWeek.Saturday) || 
                    (travelDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    decimal fuelWithAddition;

                    if(car == 'A')
                    {
                        fuelWithAddition = CalculatePercentagePriceChange(totalFuel, 0.88m, true);
                        totalFuel = fuelWithAddition;
                    } 
                    else
                    {
                        fuelWithAddition = CalculatePercentagePriceChange(totalFuel, 1.01m, true);
                        totalFuel = fuelWithAddition;
                    }
                }
            }
            else
            {         
                decimal percentageValueOfMoney = totalFuel * moneyPercentage/100m;
                decimal discount = CalculatePercentagePriceChange(percentageValueOfMoney, 0.06m, false);
                totalFuel = discount + (totalFuel - percentageValueOfMoney);
                    
                if((car == 'B') && (travelDate.DayOfWeek == DayOfWeek.Tuesday))
                {
                    decimal dieselAddition = CalculatePercentagePriceChange(totalFuel, 0.07m, true);
                    totalFuel = dieselAddition;
                }
            }
        
            totalExpense = totalFuel + totalToll;

            return decimal.Round(totalExpense, 2);
        
        }

        // Calcula o pedágio total
        static decimal CalculateTotalToll(DateTime travelDate, char car, decimal baggageWeight)
        {
            decimal totalToll = 0m;

            // Carro A
            if (car == 'A')
            {
                //Pedágio 1:
                if ((travelDate.DayOfWeek == DayOfWeek.Monday) || 
                    (travelDate.DayOfWeek == DayOfWeek.Friday) &&
                    (travelDate.Day != 12))
                {
                    totalToll += CalculateToll(1.92m, 0.19m, baggageWeight);
                }
                else if(((travelDate.DayOfWeek == DayOfWeek.Tuesday) && (travelDate.Month == 10)) || 
                        ((travelDate.DayOfWeek == DayOfWeek.Wednesday) && (travelDate.Year == 2025)))
                {
                    totalToll += CalculateToll(1.91m, 0.22m, baggageWeight);
                }
                else if((travelDate.DayOfWeek == DayOfWeek.Saturday) ||
                        (travelDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    totalToll += CalculateToll(1.97m, 0.23m, baggageWeight);
                }
                else
                {
                    totalToll += 2.16m;
                }

                //Pedágio 2:
                if ((travelDate.DayOfWeek == DayOfWeek.Monday) || 
                    (travelDate.DayOfWeek == DayOfWeek.Friday) ||
                    (travelDate.DayOfWeek == DayOfWeek.Thursday))
                {
                    totalToll += CalculateToll(1.97m, 0.20m, baggageWeight);
                }
                else
                {
                    totalToll += 2.10m;
                }

            }
            // Carro B
            else
            {
                //Primeiro pedágio:
                if(((travelDate.DayOfWeek == DayOfWeek.Thursday) && (travelDate.Month == 3)) || (travelDate.Day == 11))
                {
                    totalToll = CalculateToll(1.82m, 0.13m, baggageWeight);
                }
                else if(travelDate.DayOfWeek == DayOfWeek.Wednesday)
                {                        
                    totalToll = 2.98m;

                    if(baggageWeight > 70)
                    {
                        totalToll = CalculateToll(2.98m, 0.11m, baggageWeight);
                    }
                
                }
                else if((travelDate.DayOfWeek == DayOfWeek.Saturday) ||
                        (travelDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    totalToll = CalculateToll(1.98m, 0.23m, baggageWeight);
                }
                else
                {
                    totalToll = 2.14m;
                }

                //Segundo pedágio:
                if(travelDate.DayOfWeek == DayOfWeek.Thursday)    
                {   
                    if(baggageWeight <= 15m)
                    {
                        totalToll += CalculatePercentagePriceChange(1.98m, 1.12m, false);
                    }
                    else if(baggageWeight <= 44)
                    {
                        totalToll += CalculatePercentagePriceChange(1.98m, 2.96m, true);
                    }
                    else
                    {
                        totalToll += 1.17m * baggageWeight;
                    }
                }        
                else
                {
                    totalToll += CalculateToll(2.22m, 0.02m, baggageWeight);
                }
            }
            
            return totalToll;
        }
        
        // Calcula o valor que será o pedágio a partir da taxa, do valor por kg da bolsa e do kg da bolsa)
        static decimal CalculateToll(decimal tollFee, decimal valuePerBag, decimal baggageWeight)
        {
            decimal total;

            total = tollFee + (valuePerBag * baggageWeight);

            return total;
        }

        // Calcula o total de combustível
        static decimal CalculateFuel(char car, DateTime travelDate, decimal litersToFill)
        {
            decimal fuelPrice;
            decimal filledTankValue;

            // Para carro A -> Gasolina
            if (car == 'A')
            {
                if(travelDate <= DateTime.Parse("09/10/2021"))
                {
                    fuelPrice = 5.81m;
                }
                else
                {
                    fuelPrice = CalculatePercentagePriceChange(5.81m, 0.79m, true);
                }
            }
            // Para carro B -> Diesel
            else
            {
                if(travelDate <= DateTime.Parse("10/10/2021"))
                {
                    fuelPrice = 4.93m;
                }
                else
                {
                    fuelPrice = CalculatePercentagePriceChange(4.93m, 0.44m, true);
                }

                if (travelDate.DayOfWeek == DayOfWeek.Thursday)
                {
                    fuelPrice = CalculatePercentagePriceChange(fuelPrice, 0.02m, false);
                }
            }

            // valorTanqueEnchido = preçoCombustível * litrosParaEncher
            filledTankValue = fuelPrice * litersToFill;

            return filledTankValue;

        }

        // Calcula o desconto ou acréscimo de algo a partir do valorAtual, percentual (a ser descontado ou não) e se é um acréscimo ou não.
        static decimal CalculatePercentagePriceChange(decimal currentValue, decimal percentage, bool addition)
        {
            decimal variation;
            decimal newValue;

            // Caso for um acréscimo
            if(addition == true)
            {
                variation = currentValue * percentage/100m;
                newValue = variation + currentValue;
            }
            // Caso for desconto
            else
            {
                variation = currentValue * percentage/100;
                newValue = currentValue - variation;
            }

            return newValue;

        }
    }
}
