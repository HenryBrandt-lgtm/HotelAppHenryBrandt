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

            // add-migration "Initial migration"
            

            App.RunProgram();

            //Copy if newer i .json

            //Rooms costumers bookings Payment

            //Iqueriable för att ladda in aktiva gäster/rum osv?

            // iquerable för läsa saker?

            // "async Task<List/IEnumerable>" innan metodnamnet på läsa in listor
            //await Task.Delay

            //.Select för att visa upp rätt saker i en Read lista

            //Lägga in customers ålder genom att bläddra i lista med först år född sen månad sen dag?

            //using ( var db = new ApplicationContext(options.Options))
            //{
            //    var customers = db.ApplicationContext   
            //        .OrderBy(c => c.Name).ToList();
            //}
            //db.SaveChanges();

            //using (var db = new ApplicationContext(options.Options))
            //{
            //    var Customer = db.Customer
            //        .First(b => b.Id = 1);
            //    customer.Name = "input";
            //    db.SaveChanges();
            //}

            //appsettings.json - properties - copy if newer

        }
    }
}