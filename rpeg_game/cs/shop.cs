using Spectre.Console;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;


namespace ShopManager
{
    class Shop
    {
        
        public static string StartShop()
        {


            return "megvevett fegyver";

        }

    }

}


// public static class Program
// {
//     public static void Main(string[] args)
//     {
//         Merchant();
//     }
    
//     public static void Merchant()
//     {
//         AnsiConsole.WriteLine("Találtál egy Árust.");
//         AnsiConsole.WriteLine("");
//         if (AnsiConsole.Confirm("Beszélsz vele!"))
//         {
//             AnsiConsole.MarkupLine("Mire van szükséged?");
//             AnsiConsole.Clear();
//             Tartalom();
//         }
//         else
//         {
//             AnsiConsole.MarkupLine("Tovább haladsz az utadon");
//         }
//     }
//     //-------------------------
    
//     public static void Tartalom()
//     {

//         // tábla
//         var table = new Table();

//         table.AddColumn("Eszközök");
//         table.AddColumn(new TableColumn("Típus").Centered());
//         table.AddColumn(new TableColumn("Árak").Centered());

//         table.AddRow("Szabja", "Kard", "1200[gold3]Đ[/]");
//         table.AddRow("Vadász íj", "Íj", "700[gold3]Đ[/]");
//         table.AddRow("Gandalf Pálca", "Bot", "800[gold3]Đ[/]");
//         table.AddRow("Kenő kés", "kés", "1000[gold3]Đ[/]");
//         AnsiConsole.Write(table);

//         //----------- választás
//         var fegyvalaszt = AnsiConsole.Prompt(
//         new SelectionPrompt<string>()
//         .PageSize(10)
//         .MoreChoicesText("[grey](Válassz föl vagy le nyillal)[/]")
//         .AddChoices(new[] {
//             "Szabja","Vadász íj", "Gandalf pálca","Kenő kés"
//         }));
//         AnsiConsole.WriteLine(fegyvalaszt);
//         if(AnsiConsole.Confirm("Biztos akarod?"))
//         {
//             // Penz - penz osszeg 
//         }
//         else
//         {
//             AnsiConsole.Clear();
//             Tartalom();
//         }
//     }
// }