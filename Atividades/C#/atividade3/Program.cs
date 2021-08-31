using System;

namespace atividade3
{        
    class Program
    {
        // Variáveis globais:
        static decimal highestGrade, classAvarage, classScore, score, retakeTestScore;
        static int approved, failed, hits;
        static int approvedWithExcellence, failedWithInefficiency;
        static int approvedRecovery, approvedByRule;
        static string approvedWithPraise, failedRecovery;
        static bool hitCondition, approvedCheck = false;

        //Função Main
        public static void Main(string[] args)
        {
            //Número do estudante:
            int noStudents;
            //Perguntar a quantidade de alunos, cujo valor deve ser maior que 0
            do
            {
                Console.Write("Digite a quantidade de alunos: ");
                noStudents = int.Parse(Console.ReadLine());
                
                if (noStudents > 0)
                {
                    StartProgram(noStudents);
                }
                else
                {
                    Console.WriteLine("A quantidade de alunos deverá ser maior ou igual a 1. ");
                }
                
            } while (noStudents < 1);
        }    

        //ComeçarPrograma:
        static void StartProgram(int noStudents)
        {
            int student = 1;
            //Enquanto o número do estudante for menor que o número de estudantes, fazer:
            while (noStudents >= student)
            {
                Console.Write($"Digite o nome do aluno {student}: ");
                string nameStudent = Console.ReadLine();
                //Chamar a função de calcular frequência:
                decimal frequencyPercentage = CalculateFrequency(nameStudent);

                Console.WriteLine($"Correção da prova do(a) aluno(a): {nameStudent}");
                //CorrigirTeste:
                CorrectTest();   
                //Se a pontuaçao do aluno for maior que 6.5 E a condição de acerto (questão 10 e 11) for true E a frequência ser maior ou igual a 75:
                if (score > 6.5m && hitCondition && frequencyPercentage >= 75m)
                {
                    approved++;                                       
                    approvedCheck = true;
                }
                else
                {   
                    //Se o aluno tiver uma frequência menor que 75:
                    if (frequencyPercentage < 75)
                    {
                        Console.WriteLine($"{nameStudent}, você foi reprovado(a) por ter uma frequência menor que 75%. Sua frequência é de: {frequencyPercentage}%");
                        failed++;
                    }
                    //Se a pontuação for igual a 6.5 E o aluno não tiver acertado as questões 10 e 11 
                    //OU a frequência ser >= que 78.25% E a pontuação dele ser menor que 6.5, mas tiver acertado ao menos 4 questões:
                    else if ((score == 6.5m && !hitCondition) || 
                             (frequencyPercentage >= 78.25m)  && 
                             (score < 6.5m && hits >= 4))
                    {
                        //Ele poderá fazer a reavaliação.
                        Console.Write($"Você não passou na primeira prova. Desejas fazer a recuperação, {nameStudent}? (S, N) ");
                        string answerStudentRecovery = Console.ReadLine().ToUpper();

                        if (answerStudentRecovery == "S")
                        {
                            //Chama a função de fazer a reavaliação.
                            TakeRetakeTest(frequencyPercentage, nameStudent);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Você escolheu não fazer a recuperação. Depois não venha reclamar!");
                            Console.ResetColor();
                            failed++;
                        }

                        if (retakeTestScore > score)
                        {
                            score = retakeTestScore;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Que pena! Você reprovou e não tem chance de fazer a recuperação, pois:");
                        Console.WriteLine(" - Ou tirasse 6.5 e não acertasse nenhuma das duas últimas questões;");
                        Console.WriteLine(" - Ou tivesse uma frequência menor que 78.25%;");
                        Console.WriteLine(" - Ou tirasse 6.5 ou mais, mas acertou menos que 4 questões;");
                        Console.ResetColor();
                        failed++;
                    }
                                        
                }
                //Chama a função de calcular maior média:
                CalculateHighestGrade(score);
                //Chama a função de checar a situação do aluno:
                CheckStudentSituation(hits, frequencyPercentage, nameStudent, approvedCheck);
                //Adiciona a média do aluno à pontuação da sala
                classScore += score;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("----------------------------------------------\n");
                Console.ResetColor();

                student++;

            }
            //Calcula a média da turma dividindo a pontuação da sala pela qntd de alunos:
            classAvarage = classScore / noStudents;
            //MostrarRespostas:
            ShowAnswers();
        }
        

        //CalcularFrequencia: Calcula a frequência do aluno.
        static decimal CalculateFrequency(string nameStudent)
        {
            bool number;
            int absences;
            decimal frequency;
            decimal absencesPercentual;
            

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nATENÇÃO! As faltas não podem ultrapassar 37 ou ser negativas.");
            Console.ResetColor();

            do
            {
                Console.Write($"Digite quantas faltas o(a) {nameStudent} teve: ");
                //Tenta transformar a string pra número e colocar o valor na variável absences (faltas):
                if (int.TryParse(Console.ReadLine(), out absences))
                {
                    number = true;
                }
                else
                {
                    Console.WriteLine("Isso não é um número!");
                    number = false;
                }
            //Se não for um número OU se as faltas forem negativas OU as faltas forem maior que 37, ele vai dar um loop
            } while (number == false || (absences < 0) || (37 < absences));


            //Calcula a frequência do aluno:
            absencesPercentual = (absences * 100) / 37;
            frequency = 100 - absencesPercentual;

            return frequency;
        }


        //CorrigirAvaliacao
        static void CorrectTest()
        {          
            score = 0;
            hits = 0;
            hitCondition = false;      
            int quest = 1;
            //Enquanto a questão for menor ou igual ao número total de questões:
            while (11 >= quest)
            {                    
                Console.Write($"Você acertou a questão {quest}? (S, N) ");
                string answerStudent =  Console.ReadLine().ToUpper();
                
                if (answerStudent == "S" || answerStudent == "N")
                {   
                    //Se a questão for de 1 até 4, vai ser somado 0.5 pontos na pontuação do aluno,
                    if (answerStudent == "S" && quest <= 4)
                    {
                        score += 0.5m;
                    }
                    //Se a questão estiver entre 5 e 9, será somado 1 ponto,
                    else if (answerStudent == "S" && quest <= 9)
                    {
                        score += 1;
                    }
                    //Se a questão for 10 ou 11, será acrescentado 1.5 ponto na pontuação e a condição se tornará verdadeira.
                    else if (answerStudent == "S")
                    {
                        score += 1.5m; 
                        hitCondition = true;
                    }

                    quest++;
                    hits++;
                }
                //Caso ele não digite S ou N, continuará na mesma questão. 
                else
                {
                    Console.WriteLine("Você não digitou nem S e nem N!");
                }
                    
            }
        }


        //FazerProvaDeRecuperacao: Realiza a prova de recuperação (na verdade a corrige):
        static void TakeRetakeTest(decimal frequencyPercentage, string nameStudent)
        {   
            bool hitConditionQuest5 = false;
            bool hitConditionQuest6 = false;
            int quest = 1;
            hits = 0;
            retakeTestScore = 0;
            //Enquanto a questão for menor ou igual ao número total de questões da prova:
            while (6 >= quest)
            {                    
                Console.Write($"\nVocê acertou a questão {quest}? (S, N) ");
                string answerStudent =  Console.ReadLine().ToUpper();

                //Se a resposta do aluno for sim ou não:
                if (answerStudent == "S" || answerStudent == "S")
                {   
                    //Se for questões de 1 a 4, adiciona 1 ponto à pontuação da recuperação:
                    if (answerStudent == "S" && quest <= 4)
                    {
                        retakeTestScore += 1;
                    }
                    //Se for a questão 5, adiciona 2.5 pontos e a condição de acerto da questão 5 se torna verdadeira:
                    else if (answerStudent == "S" && quest == 5)
                    {
                        retakeTestScore += 2.5m; 
                        hitConditionQuest5 = true;
                    }
                    //Se for a questão 6...
                    else if (answerStudent == "S")
                    {
                        retakeTestScore += 3.5m; 
                        hitConditionQuest6 = true;
                    }

                    quest++;                
                    hits++;
                }
                else
                {
                    Console.WriteLine("Você não digitou nem S e nem N!");
                }
                    
            }

            //Se o aluno (acertar a questão 5 E 6) OU (a pontuação dele for maior que 6.5 E ter acertado a questão 5) 
            //OU (ter acertado a questão 6 E ter tirado no mínimo 6.5):
            if ((hitConditionQuest5 && hitConditionQuest6) || 
                (retakeTestScore == 6.5m && hitConditionQuest5) ||
                (hitConditionQuest6 && retakeTestScore >= 6.5m))
                {
                    approved++;
                    approvedRecovery++;
                    approvedCheck = true;
                }
                //Se ele tiver acertado a questão 6 E a pontuação dele for igual ou maior a 5.5 
                //(ter acertado pelo menos 2 questões de 1 ponto) E sua frequência ser maior ou igual a 82.85%:
                else if (hitConditionQuest6 && retakeTestScore >= 5.5m && frequencyPercentage >= 82.85m)
                {
                    retakeTestScore = 3.5m + score;
                    
                    //Se a pontuação da última prova + pontuação da sexta questão (3.5) 
                    //for maior que ou igual a 7, ele será aprovado por regra específica.
                    if (retakeTestScore >= 7)
                    {
                        approved++;
                        approvedByRule++;
                        approvedCheck = true;
                    }
                }
                //Senão ele reprova
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nQue pena! Você não conseguiu recuperar sua nota.");
                    Console.ResetColor();
                    failedRecovery += $" - {nameStudent}\n";
                    failed++;
                }
        }


        //CalcularMaiorMedia: calcula a maior média entre os alunos.
        static void CalculateHighestGrade(decimal score)
        {
            //Verifica se a pontuação do aluno é maior que a maior nota que teve até então. 
            if (score > highestGrade)
            {
                //Se for, a pontuação do aluno será a maior nota da turma.
                highestGrade = score;
            }
        }


        //ChecarSituacaoAluno: verifica se ele será aprovado com excelência, louvor ou se será reprovado por ineficiência.
        static void CheckStudentSituation(int hits, decimal frequencyPercentage, string nameStudent, bool approvedCheck)
        {
            //Se ele acertou todas as questões da primeira prova e foi aprovado, ele será aprovado com excelência
            if (hits == 11 && approvedCheck)
            {   
                //Se ele tiver uma frequência maior ou igual a 98, ele será aprovado com louvor.
                if (frequencyPercentage >= 98m)
                {
                    approvedWithPraise += $" - {nameStudent}\n";
                }

                approvedWithExcellence++;
            }
            //Se ele tiver acertado 3 questões ou menos E tiver sido reprovado, ele será reprovado por ineficiência.
            else if (hits <= 3 && !approvedCheck)
            {
                failedWithInefficiency++;
            }
        }


        //Mostrar respostas:
        static void ShowAnswers()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Resultados: \n");
            Console.ResetColor();

            Console.WriteLine("Total de aprovados com excelência: " + approvedWithExcellence);
            Console.WriteLine("Quantidade de aprovados: " + approved);
            Console.WriteLine("Total de aprovados na recuperação: " + approvedRecovery);
            Console.WriteLine("Total de aprovados por regra específica: " + approvedByRule);
            Console.WriteLine("Quantidade de reprovados: " + failed);
            Console.WriteLine("Média geral da turma: " + classAvarage);
            Console.WriteLine("Maior média registrada: " + highestGrade);
            Console.WriteLine("Total reprovados que acertaram 3 ou menos questões: " + failedWithInefficiency);
            
            if (!string.IsNullOrEmpty(approvedWithPraise))
            {
                Console.WriteLine("\nAlunos aprovados com louvor: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(approvedWithPraise);
                Console.ResetColor();
                Console.WriteLine("Parabéns, vocês são muito dedicados!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\nNenhum aluno foi aprovado com louvor. :(");
                Console.ResetColor();
            }

            if (!string.IsNullOrEmpty(failedRecovery))
            {
                Console.WriteLine("\nAlunos que reprovaram na recuperação:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(failedRecovery);
                Console.ResetColor();  
                Console.WriteLine("Infelizmente vocês não conseguiram passar na recuperação. Boa sorte na próxima vez!");     
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nNão houve nenhum aluno que reprovou na recuperação! :)");
                Console.ResetColor();
            }
        }
    }
}
