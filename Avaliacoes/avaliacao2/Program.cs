using System;

namespace avaliacao2
{
    class Program
    {
        //Variáveis globais:
        static int qntdNota200, qntdNota100, qntdNota50, qntdNota20, qntdNota10, qntdNota5, qntdNota2;
        static int contadorNotas200, contadorNotas100, contadorNotas50, contadorNotas20, contadorNotas10, contadorNotas5, contadorNotas2;
        static int valorAcumulado, valorParaSaque;
        static decimal saldoDisponivel = qntdNota200 + qntdNota100 + qntdNota50 + qntdNota20 + qntdNota10 + qntdNota5 + qntdNota2;
        static bool sucessoSaque;
        static string diaSemanaHoje = DateTime.Today.DayOfWeek.ToString();        

        //Método main: Faz a primeira pergunta do programa. Se o usuário não digitar 1, 2 ou 0, ele repete a pergunta.
        static void Main(string[] args)
        {
            int opcao;
            bool repetidor = true;
            
            do
            {
                Console.WriteLine("\nDigite 1 para depositar, 2 para sacar um valor ou 0 para sair.");
                
                if (int.TryParse(Console.ReadLine(), out opcao) && (opcao == 1 || opcao == 2 || opcao == 0))
                {   
                    switch (opcao)
                    {   
                        case 1: DepositarNota(); continue;
                        case 2: SacarValor(); continue;
                        case 0: return;
                    }
                }    
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida!");
                    Console.ResetColor();
                } 
            
            } while (repetidor);
        }

        //Método da opção de depositar a nota: 
        static void DepositarNota()
        {
            int nota;
            int quantidade;
            bool numeroValidoNota;
            bool numeroValidoQuantidade;

            do
            {
                Console.WriteLine("\nDigite uma nota para depositar ou 0 para voltar ao menu anterior.");
                numeroValidoNota = int.TryParse(Console.ReadLine(), out nota);
                //Se a nota que o usuário digitar for uma nota existente (200, 100, 50, 20, 10, 5, 2):
                if (nota == 200 || nota == 100 || nota == 50 || nota == 20 || nota == 10 || nota == 5 || nota == 2)
                {
                    do
                    {   //Ele pedirá para o usuário digitar uma quantia dessa nota:
                        Console.WriteLine($"Digite uma quantidade para a nota de {nota} reais ou 0 para voltar ao menu anterior.");
                        numeroValidoQuantidade = int.TryParse(Console.ReadLine(), out quantidade);
                        //Se a quantia que ele digitou for um número e ele for maior que 0, ele chamará a função de atualizar a quantidade da nota:
                        if (numeroValidoQuantidade && quantidade > 0)
                        {   
                            AtualizarQuantidadeNota(nota, quantidade); 
                            break;               
                        }
                        //Se a quantia for um número inteiro e for igual a 0, ele voltará ao menu inicial:
                        else if (numeroValidoQuantidade && quantidade == 0)
                        {
                            break;
                        }
                        //Se não for um número a quantia, nem for nenhuma das notas ou 0, ele printará a mensagem de erro:
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Quantidade inválida! Tente novamente.");
                            Console.ResetColor();
                        }  
                        //A variável que verifica se é um número inteiro se tornará false para que o loop aconteça:
                        numeroValidoQuantidade = false;

                    } while (!numeroValidoQuantidade);
                                  
                }
                // Se o usuário digitar 0, ele voltará para o menu inicial:
                else if (nota == 0 && numeroValidoNota)
                {
                    return;
                }
                //Se o usuário não digitar um número inteiro válido ou 0, dará essa mensagem de erro:
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valor inválido! Tente novamente.");
                    Console.ResetColor();
                }
                //A variável que verifica se é um número inteiro se tornará falsa para que o loop aconteça:
                numeroValidoNota = false;
                
            } while (!numeroValidoNota);
        }

        //Método que atuliza a quantidade de notas. Tem como parâmetros a nota requerida pelo usuário e a quantidade que ele quer dessa nota:
        static void AtualizarQuantidadeNota(int nota, int quantidade)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"A quantidade da nota de {nota} reais foi atualizada para:");
            Console.ResetColor();
            //Faz um switch para verificar qual nota o usuário escolheu. Depois adiciona a quantidade à variável global da nota escolhida.
            switch (nota)
            {
                case 200: 
                    qntdNota200 += quantidade;
                    Console.WriteLine(qntdNota200);
                    break;
                case 100: 
                    qntdNota100 += quantidade;
                    Console.WriteLine(qntdNota100);
                    break;
                case 50: 
                    qntdNota50 += quantidade;
                    Console.WriteLine(qntdNota50);
                    break;
                case 20: 
                    qntdNota20 += quantidade;
                    Console.WriteLine(qntdNota20);
                    break;
                case 10: 
                    qntdNota10 += quantidade;
                    Console.WriteLine(qntdNota10);
                    break;
                 case 5: 
                    qntdNota5 += quantidade;
                    Console.WriteLine(qntdNota5);
                    break;
                 case 2: 
                    qntdNota2 += quantidade;
                    Console.WriteLine(qntdNota2);
                    break;
            } 
        }

        //Método da opção de sacar algum valor:
        static void SacarValor()
        {
            int opcao;
            int valorSaque;
            //Sempre zera as variáveis globais auxiliares que contém a quantidade de notas e dps calcula o saldo disponível primeiro:
            ZerarContadoresDeNotas();
            CalcularSaldoDisponivel();
            //Verifica se tem saldo:
            if (saldoDisponivel > 0)
            { 
                Console.WriteLine("Digite o valor que deseja sacar:");
                
                if ((int.TryParse(Console.ReadLine(), out valorSaque) && (valorSaque > 0)))
                {   
                    //Verifica se o valor que o usuário quer sacar está dentro das regras de saque:
                    if ((valorSaque > 1000) && (diaSemanaHoje == DayOfWeek.Saturday.ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Operação não permitida! Aos sábados o valor máximo para saque é de R$ 1.000,00");
                        Console.ResetColor(); 
                    }
                    else if ((valorSaque > 800) && (diaSemanaHoje == DayOfWeek.Sunday.ToString()))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Operação não permitida! Aos domingos o valor máximo para saque é de R$ 800,00");
                        Console.ResetColor(); 
                    }
                    else
                    {
                        Console.WriteLine("Digite 1 para especificar as notas que deseja receber, 2 para prosseguir ou 0 para voltar.");
                        bool opcaoValida = int.TryParse(Console.ReadLine(), out opcao);

                        if (saldoDisponivel >= valorSaque)
                        {
                            if (opcaoValida && (opcao >= 0 && opcao <= 2))
                            {
                                string fraseDeErro = "";
                                //Se o usuário quiser especificar as notas, o programa vai chamar o método EspecificarNotas, se ela prosseguir, vai ser
                                //chamado o método EscolherNotasAutomaticamente. Cada função tem um tipo de mensagem de erro diferente:
                                switch(opcao)
                                {
                                    case 1: EspecificarNotas(valorSaque); fraseDeErro = "Impossível compor o valor solicitado!"; break;
                                    case 2: EscolherNotasAutomaticamente(valorSaque); fraseDeErro = "Não foi possível montar o valor solicitado!"; break;
                                    case 0: return;
                                }
                                //Se o valor obtido em ambas as funções for menor que o valor que o usuario quer sacar, vai printar a mensagem de erro
                                //e mostrar a quantidade de notas que tem no caixa.
                                if ((valorAcumulado < valorSaque) && sucessoSaque)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(fraseDeErro);
                                    Console.ResetColor(); 

                                    MostrarQuantidadeNotas();
                                }
                                //Se caso tudo der certo, ele vai printar a mensagem de sucesso, chamar o método 
                                //CalcularSaldoDisponivel e depois o MostrarQuantidadesNotas:
                                else if (sucessoSaque)
                                {
                                    if (PedirEVerificarSenha())
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Saque realizado com sucesso!");
                                        Console.ResetColor();                               
                                        
                                        CalcularSaldoDisponivel();
                                        MostrarQuantidadeNotas();
                                    }
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Opção de saque inválida!");
                                Console.ResetColor();  
                            } 
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Saldo indisponível!");
                            Console.ResetColor();
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Valor inválido para saque!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Saldo indisponível!");
                Console.ResetColor();

                MostrarQuantidadeNotas();

            }                
        }
        
        //Método de calcular o saldo disponível:
        static void CalcularSaldoDisponivel()
        {
            //Ele desconta de cada variável global que tem a quantidade das seguintes notas, a quantidade que o usuário ou o programa retirou:
            qntdNota200 -= contadorNotas200;
            qntdNota100 -= contadorNotas100;
            qntdNota50 -= contadorNotas50;
            qntdNota20 -= contadorNotas20;
            qntdNota10 -= contadorNotas10;
            qntdNota5 -= contadorNotas5;
            qntdNota2 -= contadorNotas2;
            //Depois multiplica cada quantidade com sua referente nota e no final soma tudo. Esse será o saldo disponível do caixa:
            saldoDisponivel = (qntdNota2 * 2) + (qntdNota5 * 5) + (qntdNota10 * 10) + 
            (qntdNota20 * 20) + (qntdNota50 * 50) + (qntdNota100 * 100) + (qntdNota200 * 200);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Saldo disponível: {saldoDisponivel.ToString("C")}.");
            Console.ResetColor();
        }

        //Método da opção de "prosseguir":
        static void EscolherNotasAutomaticamente(int valorSaque)
        {
            //Começa com uma variável auxiliar (valorParaSaque) que recebe o valor que o usuário quer sacar:
            valorAcumulado = valorSaque;
            valorParaSaque = valorSaque;
            sucessoSaque = true;
            //Verifica se é quarta feira, se for, a nota vai começar no 100 por conta da regra de que quartas não se saca notas de 200:
            int nota = (diaSemanaHoje == DayOfWeek.Wednesday.ToString() ? 100 : 200);
            //Para cada tipo de nota, é chamado o método CalcularNotasAutomaticamente:
            while (nota >= 2)
            {
                switch(nota)
                {
                    case 200: CalcularNotasAutomaticamente(qntdNota200, out contadorNotas200, nota); break;
                    case 100: CalcularNotasAutomaticamente(qntdNota100, out contadorNotas100, nota); break;
                    case 50: CalcularNotasAutomaticamente(qntdNota50, out contadorNotas50, nota); break;
                    case 20: CalcularNotasAutomaticamente(qntdNota20, out contadorNotas20, nota); break;
                    case 10: CalcularNotasAutomaticamente(qntdNota10, out contadorNotas10, nota); break;
                    case 5: CalcularNotasAutomaticamente(qntdNota5, out contadorNotas5, nota); break;
                    case 2: CalcularNotasAutomaticamente(qntdNota2, out contadorNotas2, nota); break;
                }
                
                nota /= 2;
                if (nota == 25)
                {
                    nota -= 5;
                }
            }            

            valorAcumulado -= valorParaSaque;
        }

        //Método que calcula a quantidade de cada nota que vai precisar para compor o valor solicitado pelo usuário:
        static void CalcularNotasAutomaticamente(int qntdNotasDisponiveis, out int contadorNotas, int nota)
        {
            int qntdNotasUsadas = 0;
            int sobra;
            //Enquanto a quantidade disponíveis da nota for maior que a variável qntdNotasUsadas, ele irá subtrair a nota do valorParaSaque:
            while (qntdNotasUsadas < qntdNotasDisponiveis)
            {
                sobra = valorParaSaque - nota;
                //Se o que sobrar dessa subtração for 1, 3 ou for menor que 0, quer dizer que ele terá que usar a próxima nota que vier:
                if (sobra == 1 || sobra == 3 || sobra < 0)
                {   
                    break;                    
                }
                //Senão, ele dimnuirá do valorParaSaque, a nota
                valorParaSaque -= nota;
                    
                qntdNotasUsadas++;
            }
            //A variável auxiliar passará a ser a quantidade de notas utilizadas que o programa usou:
            contadorNotas = qntdNotasUsadas;
        }

        //Método da opção de "especificar notas":
        static void EspecificarNotas(int valorSaque)
        {   
            //Desta vez o valorAcumulado começa em 0:
            sucessoSaque = true;
            valorAcumulado = 0;
            int nota = (diaSemanaHoje == DayOfWeek.Wednesday.ToString() ? 100 : 200);
            int qntdNota;            
            //Enquanto ele for menor que o valor do saque E a nota for maior ou igual a 2, ele pedirá a quantidade da nota:
            while (valorAcumulado < valorSaque && nota >= 2)
            {
                Console.WriteLine($"Digite a quantidade de notas de {nota} reais:");

                if ((int.TryParse(Console.ReadLine(), out qntdNota) && (qntdNota >= 0)))
                {   
                    //Para cada nota, ele chama o método CalcularAcúmulo:
                    switch (nota)
                    {
                        case 200: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota200, out contadorNotas200); break;
                        case 100: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota100, out contadorNotas100); break;
                        case 50: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota50, out contadorNotas50); break;
                        case 20: CalcularAcumulo(qntdNota, nota, valorSaque,qntdNota20, out contadorNotas20); break;
                        case 10: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota10, out contadorNotas10); break;
                        case 5: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota5, out contadorNotas5); break;
                        case 2: CalcularAcumulo(qntdNota, nota, valorSaque, qntdNota2, out contadorNotas2); break;
                    }
                    //Se der algum erro, ele volta ao menu inicial:
                    if (!sucessoSaque)
                    {
                        break;
                    }

                    nota /= 2;
                    if (nota == 25)
                    {
                        nota -= 5;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Quantidade inválida! Tente novamente!");
                    Console.ResetColor();
                }
            }
        }

        //Método que calcula as notas * quantidade que o usuário informou
        static void CalcularAcumulo(int quantidadeNota, int nota, int valorSaque, int qntdNotasDisponiveis, out int contadorNota)
        {
            //Se a quantdade que ele quis de notas for maior que as que o caixa tem, dá a seguinte mensagem de erro:
            if (quantidadeNota > qntdNotasDisponiveis)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quantidade de notas é superior a quantidade disponível!");
                Console.ResetColor();
                MostrarQuantidadeNotas();
                sucessoSaque = false;
                contadorNota = 0;
            }
            //Senão ele acrescenta ao valorAcumulado a quantidade que o usuário quis * a nota:
            else
            {
                valorAcumulado += quantidadeNota * nota;
                //Se o valorAcumulo passar do valor de saque, será impossível continuar:
                if (valorAcumulado > valorSaque)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Esta quantidade vai gerar um valor que supera o valor solicitado! Não é possível continuar!");
                    Console.ResetColor();  
                    sucessoSaque = false;
                }
                //No fim a variável auxiliar, que guarda as quantidades de notas usadas, vaireceber a quantia que o usuário informou:
                contadorNota = quantidadeNota;
            }
        }

        //Função que pede e verifica a senha pro usuário, se for sábado ou domingo, ele só terá 3 chances pra acertar, senão, 4.
        static bool PedirEVerificarSenha()
        {
            int senha;
            int tentativasRestantes =  ((diaSemanaHoje == DayOfWeek.Saturday.ToString()) || (diaSemanaHoje == DayOfWeek.Sunday.ToString()) ? 3 : 4);           

            do
            {
                Console.WriteLine("Digite sua senha:");
                //Verifica se é maior que 10000 e menor que 10060:
                if ((int.TryParse(Console.ReadLine(), out senha)) && (senha > 10000 && senha < 10060))
                {
                    return true;
                }
                else
                {   
                    tentativasRestantes--; 

                    if (tentativasRestantes == 0)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Senha inválida! Você ainda tem: {tentativasRestantes} tentativa(s).");
                    Console.ResetColor();                    
                }

            } while (tentativasRestantes > 0);
            //Se errar todas as tentativas:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"As tentativas de digitação de senhas foram esgotadas. O Saque não poderá ser realizado!");
            Console.ResetColor(); 

            ZerarContadoresDeNotas();

            return false;    
        }

        static void ZerarContadoresDeNotas()
        {
            contadorNotas200 = 0;
            contadorNotas100 = 0;
            contadorNotas50 = 0;
            contadorNotas20 = 0;
            contadorNotas10 = 0;
            contadorNotas5 = 0;
            contadorNotas2 = 0;
        }
        
        static void MostrarQuantidadeNotas()
        {   
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nQuandidade disponível de notas de R$ 200: {qntdNota200}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 100: {qntdNota100}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 50: {qntdNota50}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 20: {qntdNota20}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 10: {qntdNota10}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 5: {qntdNota5}");
            Console.WriteLine($"Quandidade disponível de notas de R$ 2: {qntdNota2}");
            Console.ResetColor();
        }
    }
}
