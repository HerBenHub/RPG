using System.Runtime.CompilerServices;
using System.Text.Json;

namespace JsonManager
{
    public class FegyverAdatok
    {
        public string? tipus { get; set; }
        public string? nev { get; set; }
        public int sebzes { get; set; }
        public string? ritkasag { get; set; }
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
            string jsonString = File.ReadAllText(fileName);

            Dictionary<string,FegyverAdatok>? summary = JsonSerializer.Deserialize<Dictionary<string,FegyverAdatok>>(jsonString);
            //json felbontása

            return summary;

        }

        public static Dictionary<string,List<string>> RitkasagLista(string? tipus)
        {

            string fileName = @"ritkasag.json";
            string jsonString = File.ReadAllText(fileName);

            RitkasagAdatok? ritkasag = JsonSerializer.Deserialize<RitkasagAdatok>(jsonString);

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


    }
}
