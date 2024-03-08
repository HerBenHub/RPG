using System;
using System.Security.Cryptography;
using System.Text.Json;
using JsonManager;
using RitkasagManager;

namespace SzovegManager
{
    public class RandomText
    {
        public void randomText()
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>()
            {
                {"Láda_megtalálva", new List<string>(){"ládanyitás", "láda otthagyása"}},
                {"Elágazás", new List<string>(){"valami"}}
            };
            var rnd = new Random();
            int num = rnd.Next();
            ////????
            
            foreach (KeyValuePair<string, List<string>> adat in dict)
            {
                string ertek = adat.Key;
                foreach (string str in adat.Value)
                {
                    string theValue = str;
                }
            }
        }
    }
}