using System;
using System.Collections.Generic;
using System.IO; 
using Newtonsoft.Json; 
namespace JsonManager
{
    
    public class FegyverAdatok
    {
        public string? tipus { get; set; }
        public string? nev { get; set; }
        public int sebzes { get; set; }
        public string? ritkasag { get; set; }
        public Dictionary<string,string>? szoveg { get; set; }
    }

    public class EllensegAdatok
    {

    }

    public class RitkasagAdatok
    {
        public string? common { get; set; }
        public string? uncommon { get; set; }
        public string? rare { get; set; }
        public string? epic { get; set; }
        public string? legendary { get; set; }

    }

    public class JsonOlvaso
    {
        public static Dictionary<string,FegyverAdatok>? FegyverLista()
        {
            string fileName = @"fegyverek.json";

            string jsonString = File.ReadAllText(fileName); //Itt nem tetszik neki valami
            Dictionary<string, FegyverAdatok>? summary = JsonConvert.DeserializeObject<Dictionary<string, FegyverAdatok>>(jsonString);
            //json felbontása

            return summary;

        }

        public static Dictionary<string,List<string>> RitkasagLista(string? tipus)
        {

            string fileName = @"ritkasag.json";
            string jsonString = File.ReadAllText(fileName);

            RitkasagAdatok? ritkasag = JsonConvert.DeserializeObject<RitkasagAdatok>(jsonString);

            Dictionary<string,List<string>> lista = new Dictionary<string,List<string>>();
            lista.Add("common",ritkasag.common.Split('/').ToList());
            lista.Add("uncommon",ritkasag.uncommon.Split('/').ToList());
            lista.Add("rare",ritkasag.rare.Split('/').ToList());
            lista.Add("epic",ritkasag.epic.Split('/').ToList());
            lista.Add("legendary",ritkasag.legendary.Split('/').ToList());
            //berakja egy könnyebben olvashato listába a megadott ritkaságokat.
            //ezt nem tudom hogy lehetne megcsinálni kevésbé undorítóra :(

            return lista;

        }
        
        //Szövegek beolvasása
        public static Dictionary<string, Dictionary<string, string>> ReadJsonFile(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonString);
        }

        public static KeyValuePair<string, string> GetRandomKeyValuePair(Dictionary<string, Dictionary<string, string>> dictionary)
        {
            Random rnd = new Random();

            List<string> mainKeys = new List<string>(dictionary.Keys);
            string selectedMainKey = mainKeys[rnd.Next(mainKeys.Count)];

            var subDictionary = dictionary[selectedMainKey];
            List<string> subKeys = new List<string>(subDictionary.Keys);

            string selectedSubKey = subKeys[rnd.Next(subKeys.Count)];
            string selectedValue = subDictionary[selectedSubKey];

            return new KeyValuePair<string, string>(selectedMainKey, selectedValue);
        }

        public static void PrintKeyValuePair(KeyValuePair<string, string> selectedKeyValuePair)
        {
            Console.WriteLine($"Választott kulcs: {selectedKeyValuePair.Key}, Alkulcs: {selectedKeyValuePair.Key}, Érték: {selectedKeyValuePair.Value}");
        }

        public static void ProcessJsonFile(string fileName)
        {
            var dictionary = ReadJsonFile(fileName);
            var selectedKeyValuePair = GetRandomKeyValuePair(dictionary);
            PrintKeyValuePair(selectedKeyValuePair);
        }

    }
}
//using newtonsoft.json!!! (JsonConvert.DeserializeObject NOT JsonSerializer.Deserialize)