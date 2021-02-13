using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace desafio_elixir_stone
{
    class Program
    {
        // Variavel para manter o console aberto
        static bool KeepRunning = true;

        // Variaveis para o código
        static List<Item> items = new List<Item>();
        static List<string> emails = new List<string>();
        static int total;
        static int gastoI;
        static int resto;

        static void Main(string[] args)
        {
            while (KeepRunning)
            {
                //Se digitar "executar" no console, ele executa o código.
                if (Console.ReadLine() == "executar")
                {
                    updateEmails();
                    updateItems();
                    updateTotal();
                    Console.Clear();
                    Console.WriteLine("====== Desafio Elixir Stone ======");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Total de emails: {0}", emails.Count);
                    Console.WriteLine("Total de centavos: {0}", total);
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("Gastos inviduais");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    //Se tiver emails no arquivo json
                    if(emails.Count > 0)
                    {
                        foreach (KeyValuePair<string, int> entry in gastoIndividual())
                        {
                            Console.WriteLine("Email: {0} Gasto: {1}", entry.Key, entry.Value);
                        }
                    }
                    //Se nao tiver emails no arquivo json
                    else { Console.WriteLine("Nenhum email encontrado."); }
                    continue;
                }
            }
        }
        public static Dictionary<string, int> gastoIndividual()
        {
            updateEmails();
            updateItems();
            updateTotal();
            Dictionary<string, int> x = new Dictionary<string, int>();
            resto = 0;
            if (emails.Count > 0)
            {
                if(total % emails.Count == 0)
                    gastoI = total / emails.Count;
                else
                {
                    gastoI = total / emails.Count;
                    resto = total % emails.Count;
                }
            }
            foreach(string e in emails)
            {
                if(resto > 0)
                {
                    if(emails.IndexOf(e) != emails.Count - 1)
                        x.Add(e, gastoI);
                    else
                        x.Add(e, gastoI + resto);
                }
                else
                {
                    x.Add(e, gastoI);
                }
            }
            return x;
        }
        #region Update Lists
        public static void updateItems()
        {
            items.Clear();
            var result = JsonConvert.DeserializeObject<List<Root>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/lista.json"));
            foreach (var i in result)
            {
                foreach (Item x in i.items)
                {
                    items.Add(x);
                }
            }
        }
        public static void updateEmails()
        {
            emails.Clear();
            var result = JsonConvert.DeserializeObject<List<Root>>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/lista.json"));
            foreach (var i in result)
            {
                foreach (string x in i.emails)
                {
                    emails.Add(x);
                }
            }
        }
        public static void updateTotal()
        {
            updateItems();
            total = 0;
            foreach (Item item in items)
            {
                total += item.Preço * item.Quantidade;
            }
        }
        #endregion
    }
}
