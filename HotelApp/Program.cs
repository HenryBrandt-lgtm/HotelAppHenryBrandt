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

            // I datainitilicer där jag seedar. Kan jag göra det till listor där jag jämför flera saker, dvs kan korta ner allt?

            // email måste kolla om den redan finns. Får inte ha samma pga invoice, skicka invoice?

            // Ska man kunna checka in ?

            // hur vill jag göra med room enums?

            //hittade bugg, lägger man till kund vid bokning så börjar bokningen om

            //flytta container from app.cs till egen klass?

            //kolla notpad med saker som finns kvar
        }
    }
}