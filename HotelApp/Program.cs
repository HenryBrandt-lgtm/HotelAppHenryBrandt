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


        }
    }
}