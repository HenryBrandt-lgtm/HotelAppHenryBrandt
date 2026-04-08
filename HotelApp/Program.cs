using HotelApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace HotelApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            App.RunProgram();

            // Lägga till extrasängar vid bokning?

            //I datainitilicer där jag seedar. Kan jag göra det till listor där jag jämför flera saker, dvs kan korta ner allt?

            //skriv i readme

            //email måste kolla om den redan finns. Får inte ha samma pga invoice

            //Ska man kunna checka in ?

            // hur vill jag göra med room enums?

            // lägga till avbokning som eget menyval?

            //hittade bugg, lägger man till kund vid bokning så börjar bokningen om
        }
    }
}