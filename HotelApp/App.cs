using HotelApp.Controllers;
using HotelApp.Data;
using HotelApp.Services;
using HotelApp.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace HotelApp
{
    public static class App
    {
        public static void RunProgram()
        {            

            Builder.Build();

            WelcomeText.WelcomeScreen();
            MainMenu.ShowMainMenu();
        }
    }
}
