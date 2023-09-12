using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecretWordConsole
{
    public class SecretWordMethods
    {
        public async Task<WordInfo> RequestWordAndDefinition()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    WordInfo wordInfo = new WordInfo();

                    wordInfo.Word = await GetWord(client);
                    wordInfo.Definition = await GetWordDefitnition(client, wordInfo.Word);

                    return wordInfo;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ocorreu um erro ao fazer a requisição HTTP: {e.Message}");
                return null;
            }
        }

        public string RemoveSpecialChar(string word)
        {
            string normalizedWord = word.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char letter in normalizedWord)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(letter);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(letter);
                }
            }
            return stringBuilder.ToString();
        }

        public async Task<string> GetWord(HttpClient client)
        {
            string randomWordApiUrl = "https://api.dicionario-aberto.net/word/pente";

            HttpResponseMessage responseWord = await client.GetAsync(randomWordApiUrl);

            responseWord.EnsureSuccessStatusCode();

            string responseWordBody = await responseWord.Content.ReadAsStringAsync();

            var resultWord = JsonConvert.DeserializeObject<List<WordInfo>>(responseWordBody);

            string randomWord = RemoveSpecialChar(resultWord[0].Word);

            return randomWord;
        }

        public async Task<string> GetWordDefitnition(HttpClient client, string randomWord)
        {
            string definitionApiUrl = $"https://api.dicionario-aberto.net/word/{randomWord}";

            HttpResponseMessage responseDefinition = await client.GetAsync(definitionApiUrl);

            responseDefinition.EnsureSuccessStatusCode();

            string responseDefinitionBody = await responseDefinition.Content.ReadAsStringAsync();
            var resultDefinition = JsonConvert.DeserializeObject<List<dynamic>>(responseDefinitionBody);

            string wordDefinition = resultDefinition[0].xml;

            XDocument xmlDoc = XDocument.Parse(wordDefinition);

            XElement defElement = xmlDoc.Descendants("def").FirstOrDefault();

            wordDefinition = defElement.ToString();

            string[] definition = wordDefinition.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            return definition[1];
        }

        public char[] CreateBlanks(int wordLenght)
        {
            char[] blanks = new char[wordLenght];
          
            for (int i = 0; i < blanks.Length; i++)
            {
                blanks[i] = '_';
            }

            return blanks;
        }

        public async Task Play()
        {
            WordInfo wordInfo = await RequestWordAndDefinition();

            Console.WriteLine();

            char[] blanks = CreateBlanks(wordInfo.Word.Length);

            char[] secretWordLettersArray = wordInfo.Word.ToCharArray();

            do
            {
                Console.WriteLine($"\nDigite uma palavra. ({blanks.Length} letras) - Digite 1 para dica");
                Console.WriteLine(blanks);
                string userInput = Console.ReadLine();

                if (userInput == "1")
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
        }
    }
}

