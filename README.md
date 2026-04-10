🏨 HotelApp

En konsolbaserad hotellapplikation byggd i C# med Entity Framework Core.
Applikationen låter användaren hantera kunder, rum, bokningar och betalningar via ett menybaserat gränssnitt.

------------------------------------------
-📌 Funktioner

-👤 Hantera kunder 

-🛏️ Hantera rum

-📅 Skapa och hantera bokningar med faktura

-🧭 Menybaserat användargränssnitt i konsolen

-💾 Databaslagring med Entity Framework Core

-🔄 Automatisk migrering och seedning av data vid start

-----------------------------------------
🧱 Projektstruktur


│

├── Controllers/          # Logik för hantering av data (Booking, Customer, Room)

│   └── MenuControllers/  # Menyhantering i konsolen

│

├── Data/                 # Databas och DbContext

│

├── Services/             # Affärslogik

│

├── UI/                   # UI-komponenter (t.ex. välkomsttext)

│

├── Program.cs            # Startpunkt

├── App.cs                # Konfiguration och uppstart

└── appsettings.json      # Konfiguration (t.ex. connection string)

-----------------------------------------
🚀 Installation & Körning
1. Klona repot
git clone <[repo-url](https://github.com/HenryBrandt-lgtm/HotelAppHenryBrandt.git)>
cd HotelApp
2. Konfigurera databas

Uppdatera appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=HotelAppDb;Trusted_Connection=True;"
}
3. Kör projektet
dotnet run

-----------------------------------------
🧪 Vad händer vid start?

När applikationen startar:

-🔧 Dependency Injection sätts upp

-🗄️ Databasen migreras automatiskt

-🌱 Data seedas (exempeldata läggs till)

-👋 En välkomstskärm visas

-📋 Huvudmenyn startar

------------------------------------------
⚠️ Kända förbättringsområden

-Input och validering av e-post för att skicka invoice

-Hantering av extrasängar

-Möjlighet till in-/utcheckning

-Förbättrad struktur för enums och logik

------------------------------------------
👨‍💻 Författare

Henry Brandt
