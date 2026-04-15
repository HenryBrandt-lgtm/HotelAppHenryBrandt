
------visar all info om customers, förutom födelsedag-----

USE HenrysHotel;
GO

SELECT 
	CustomerId,
	FirstName,
	LastName,	
	IsActive
FROM Customers

-----visar samma info om kunder men där enast aktiva(icke raderade kunder) visas-----

USE HenrysHotel;
GO

SELECT 
	CustomerId,
	FirstName,
	LastName
FROM Customers
WHERE IsActive = 1;

----visar all info om alla rum med dyrste rummet först-----------

USE HenrysHotel
GO

SELECT * 
FROM Rooms
ORDER BY Price DESC;

-----visar bokningar med namn på kund som bokat samt vilket rum som är bokat med pris----

USE HenrysHotel
GO

SELECT 
    Customers.FirstName,
    Customers.LastName,
    Bookings.BookingId,
    Bookings.StartDate,
    Bookings.EndDate,
    Rooms.RoomName,
    Rooms.Price
FROM Bookings
JOIN Customers
    ON Bookings.CustomerId = Customers.CustomerId
JOIN Rooms
    ON Bookings.RoomId = Rooms.RoomId;


-----Visar hur mycket varje kund bokat rum och betalat för totalt------

USE HenrysHotel
GO

SELECT 
    C.CustomerId,
    C.FirstName,
    C.LastName,
    SUM(I.Amount) AS TotalAmount,
    I.IsPaid
FROM Customers C
JOIN Bookings B
    ON C.CustomerId = B.CustomerId
JOIN Invoices I
    ON B.BookingId = I.BookingId
WHERE I.IsPaid = 1
GROUP BY 
    C.CustomerId,
    C.FirstName,
    C.LastName,
    I.IsPaid;
    

-----visar kunder som betalt boknignar för totalt över 1000kr----
USE HenrysHotel
GO

SELECT 
    FirstName,
    LastName
FROM Customers
WHERE CustomerId IN (
    SELECT 
    b.CustomerId
    FROM Bookings b
    JOIN Invoices i 
        ON b.BookingId = i.BookingId
    WHERE i.IsPaid = 1
    GROUP BY b.CustomerId
    HAVING SUM(i.Amount) > 1000);