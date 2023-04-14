-- Customers table
CREATE TABLE Customers (
    customer_id INT PRIMARY KEY CLUSTERED,
    first_name NVARCHAR(50),
    last_name NVARCHAR(50),
    email NVARCHAR(50),
    phone_number NVARCHAR(20)
);

-- Trains table
CREATE TABLE Trains (
    train_id INT PRIMARY KEY CLUSTERED,
    train_name NVARCHAR(50),
    max_capacity INT
);

-- Stations table
CREATE TABLE Stations (
    station_id INT PRIMARY KEY CLUSTERED,
    station_name NVARCHAR(50),
    city NVARCHAR(50),
    state NVARCHAR(50),
    country NVARCHAR(50)
);

-- Tickets table
CREATE TABLE Tickets (
    ticket_id INT PRIMARY KEY CLUSTERED,
    customer_id INT FOREIGN KEY REFERENCES Customers(customer_id),
    train_id INT FOREIGN KEY REFERENCES Trains(train_id),
    departure_station_id INT FOREIGN KEY REFERENCES Stations(station_id),
    arrival_station_id INT FOREIGN KEY REFERENCES Stations(station_id),
    departure_time DATETIME,
    arrival_time DATETIME,
    price DECIMAL(10,2)
);

CREATE NONCLUSTERED INDEX IX_Tickets_train_id 
    ON Tickets(train_id);
CREATE NONCLUSTERED INDEX IX_Tickets_departure_station_id 
    ON Tickets(departure_station_id);
CREATE NONCLUSTERED INDEX IX_Tickets_arrival_station_id 
    ON Tickets(arrival_station_id);

-- Payment Types table
CREATE TABLE Payment_Types (
    payment_type_id INT PRIMARY KEY CLUSTERED,
    payment_type_name NVARCHAR(50)
);

-- Sales table
CREATE TABLE Sales (
    sale_id INT PRIMARY KEY CLUSTERED,
    ticket_id INT FOREIGN KEY REFERENCES Tickets(ticket_id),
    payment_type_id INT FOREIGN KEY REFERENCES Payment_Types(payment_type_id),
    sale_date DATETIME
);

CREATE NONCLUSTERED INDEX IX_Sales_ticket_id 
    ON Sales(ticket_id);
CREATE NONCLUSTERED INDEX IX_Sales_payment_type_id 
    ON Sales(payment_type_id);