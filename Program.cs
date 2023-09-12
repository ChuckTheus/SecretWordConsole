using System;
using System.Linq;
using System.Threading.Tasks;

namespace SecretWordConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string menuOption;

            do
            {
                Console.Clear();

                Console.WriteLine("||======================== Bem vindo à cópia do Termo! ========================||\n" +
                "|| 1 - Jogar                                                                   ||\n" +                                                                               
                "|| 2 - Ler as regras                                                           ||\n" +
                "|| 3 - Agradecimentos                                                          ||\n" +
                "|| 4 - Sair                                                                    ||"
                 );
                Console.WriteLine("||=============================================================================||");

                menuOption = Console.ReadLine();

                switch (menuOption)
                {
                    case "1":
                        Console.WriteLine("||============================== CARREGANDO... ================================||");

                        WordInfo wordInfo = await new SecretWordMethods().RequestWordAndDefinition();

                        Console.WriteLine();

                        char[] blanks = new char[wordInfo.Word.Length];
                        for(int i = 0; i< blanks.Length; i++)
                        {
                            blanks[i] = '_';
                        }

                        char[] secretWordLettersArray = wordInfo.Word.ToCharArray();

                        do
                        {
                            Console.WriteLine($"\nDigite uma palavra. ({blanks.Length} letras) - Digite 1 para dica");
                            Console.WriteLine(blanks);
                            string userInput = Console.ReadLine();

                            if(userInput == "1")
                            {
                                Console.WriteLine("Dica: " + wordInfo.Definition);
                            }
                            else if (userInput.Length != blanks.Length)
                            {
                                Console.WriteLine("Tamanho de palavra inválido");
                            }
                            else
                            {
                                char[] userInputToArray = userInput.ToCharArray();

                                for (int i = 0; i < secretWordLettersArray.Length; i++)
                                {
                                    if (char.ToUpper(userInputToArray[i]) == char.ToUpper(secretWordLettersArray[i]))
                                    {
                                        blanks[i] = char.ToUpper(userInputToArray[i]);
                                    }
                                }
                                Console.WriteLine(blanks);
                            }
                        } while (blanks.Contains('_'));

                        Console.WriteLine("\nParabéns, você acertou a palavra...\nVocê será redirecionado ao menu principal, pressione qualquer tecla para continuar...");
                        Console.ReadKey();

                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("* Descubra a palavra certa, depois de cada tentativa, as peças mostram o quão perto você está da solução\n* Os acentos são retirados automaticamente\n* As letras são preenchidas de acordo com sua lacuna");
                        Console.WriteLine("\nPressione Enter para voltar ao menu...");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("\nAgradecimento especial ao meu amigo Reinaldo, que propôs a ideia");
                        Console.WriteLine("\nPressione Enter para voltar ao menu...");
                        Console.ReadKey();
                        break;
                    case "4":
                        break;
                    default:
                        break;
                }
            } while (menuOption != "4");

        }
    }
}
