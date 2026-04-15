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

├── Models                # Tables (Booking, Customer, Room, Invoice)

│

├── Services/             # Affärslogik

│

├── UI/                   # UI-komponenter (t.ex. välkomsttext)

│

├── Program.cs            # Startpunkt

├── App.cs                # Konfiguration och uppstart

└── appsettings.json      # Konfiguration

-----------------------------------------
🚀 Installation & Körning
1. Klona repot
git clone <[repo-url](https://github.com/HenryBrandt-lgtm/HotelAppHenryBrandt.git)>
cd HotelApp
2. Konfigurera databas
3. Kör projektet

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

-Skicka Invoice via email

-Möjlighet till in-/utcheckning

-Möjlighet att alltid backa ur delar av programmet om man ångrar sig

------------------------------------------
👨‍💻 Författare

Henry Brandt
