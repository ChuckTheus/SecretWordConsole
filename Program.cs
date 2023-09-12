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

                        SecretWordMethods secretWordMethods = new SecretWordMethods();

                        await secretWordMethods.Play();

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
