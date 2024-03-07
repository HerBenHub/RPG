using System.Runtime.CompilerServices;
using System.Text.Json;
using JsonManager;
using System;

namespace RitkasagManager
{

    class RitkasagSzamolo
    {
        public static string? LadaLootGenerate(string? lada_ritkasag)
        {
            var rand = new Random();

            Int32 szam = rand.Next(1,100);
            // Int32 szam = 1; //ez csak tesztelésre

            string kapott_ritkasag = "";

            Dictionary<string,List<string>> rit_lista = JsonOlvaso.RitkasagLista(lada_ritkasag);
            //ritkaság lista beolvasása
            Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            //fegyverlista beolvasása

            List<string> adott_ritkasag = rit_lista[lada_ritkasag];
            //a method\eljárásban megadott láda ritkasága

            Int32 szamlalo = 0;

            for (Int32 i = 0; i < adott_ritkasag.Count;i++)
            { 
                szamlalo += Int32.Parse(adott_ritkasag[i]);
                //összeadja a json-ban lévő százalékszámokat és ha a kiválasztott random szám alacsonyabb
                //akkor kiválasztja az aktuális ritkaságot
                if (szam <= szamlalo) {
                    kapott_ritkasag = rit_lista.Keys.ToList()[i];
                    System.Console.WriteLine($"A kiválasztott ({szam}) szám a {szamlalo} tartományában van, így a kiválasztott ritkaság \"{kapott_ritkasag}\"");
                    
                    break;
                }
            }

            return kapott_ritkasag;
        }
    }
}
