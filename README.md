🏨 HotelApp

En konsolbaserad hotellapplikation byggd i C# med Entity Framework Core.
Applikationen låter användaren hantera kunder, rum, bokningar och betalningar via ett menybaserat gränssnitt.

------------------------------------------
📌 Funktioner
👤 Hantera kunder (skapa, visa, etc.)
🛏️ Hantera rum
📅 Skapa och hantera bokningar
🧭 Menybaserat användargränssnitt i konsolen
💾 Databaslagring med Entity Framework Core
🔄 Automatisk migrering och seedning av data vid start

-----------------------------------------
🧱 Projektstruktur
HotelApp/
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
🧪 Vad händer vid start?

När applikationen startar:

🔧 Dependency Injection sätts upp
🗄️ Databasen migreras automatiskt
🌱 Data seedas (exempeldata läggs till)
👋 En välkomstskärm visas
📋 Huvudmenyn startar

------------------------------------------
⚠️ Kända förbättringsområden

Validering av t.ex. e-post (unika kunder)
Hantering av extrasängar
Möjlighet till in-/utcheckning
Bugg vid skapande av kund under bokning
Förbättrad struktur för enums och logik

------------------------------------------
👨‍💻 Författare

Henry Brandt
