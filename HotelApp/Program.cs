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

            //flytta container from app.cs till egen klass?

            //kolla notpad med saker som finns kvar

            // fixa med nvarchar grejer och se till att det inte går att skriva in mer än 255 tecken i email tex.
        }
    }
}