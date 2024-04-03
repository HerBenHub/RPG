using System.Runtime.CompilerServices;
using System.Text.Json;
using JsonManager;
using System;
using Newtonsoft.Json; 
using static InventoryManager.Items;
namespace RitkasagManager
{

    class RitkasagSzamolo
    {
        public static string? LootGenerate(string? lada_ritkasag, int? valaszt = null) 
        {
            var rand = new Random();

            Int32 szam = rand.Next(1,100);
            // Int32 szam = 1; //ez csak tesztelésre

            string kapott_ritkasag = "";

            Dictionary<string,List<string>> rit_lista = JsonOlvaso.RitkasagLista();
            //ritkaság lista beolvasása
            Dictionary<string,FegyverAdatok>? fegyverek = JsonOlvaso.FegyverLista();
            //fegyverlista beolvasása
            //Páncélok beolvasása
            Dictionary<string, PancelAdatok>? pancelok = JsonOlvaso.PancelLista();


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
                    // System.Console.WriteLine($"A kiválasztott ({szam}) szám a {szamlalo} tartományában van, így a kiválasztott ritkaság \"{kapott_ritkasag}\"");
                    
                    break;
                }
            }

            ////////////////////////////////////////////////////////////////////////////////
            Random random = new Random();
            if (valaszt==null) {valaszt = random.Next(1, 2);}
            
            List<string> kivalasztott = new List<string>();

            if (valaszt == 1)
            {
                foreach (var elemek in fegyverek)
                {
                    if (elemek.Value.ritkasag == kapott_ritkasag)
                    {
                        kivalasztott.Add(elemek.Key);
                    }
                }
            }
            if (valaszt == 2)
            {
                foreach (var elemek in pancelok)
                {
                    if (elemek.Value.ritkasag == kapott_ritkasag)
                    {
                        kivalasztott.Add(elemek.Key);
                    }
                }

            }
            

            int num = rand.Next(0,kivalasztott.Count());
            // System.Console.WriteLine($"{valaszt} : {kapott_ritkasag} : {kivalasztott.Count()} : {num}"); //debug
            
            //A két listát bele kéne rakni úgy egy harmadikba, hogy azt tudjam returnolni, majd ha az egyik allistában
            //megváltozik valami az rögtön a folyamatok legvégén, a harmadik listában is megváltozzon.
                
            return kivalasztott[num];
        }
    }
}
