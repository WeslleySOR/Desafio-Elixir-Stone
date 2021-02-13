using System;
using System.Collections.Generic;
using System.Text;

namespace desafio_elixir_stone
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Item
    {
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int Preço { get; set; }
    }

    public class Root
    {
        public List<Item> items { get; set; }
        public List<string> emails { get; set; }
    }

}
