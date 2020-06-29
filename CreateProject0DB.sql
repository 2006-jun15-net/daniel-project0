CREATE TABLE Customer(
	CustomerID INT IDENTITY(1, 1) NOT NULL,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_Customer PRIMARY KEY (CustomerID)
);

CREATE TABLE Location(
	LocationID INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	Address NVARCHAR(200) NULL,
	CONSTRAINT PK_Location PRIMARY KEY (LocationID)
);

CREATE TABLE Product(
	ProductID INT IDENTITY(1, 1) NOT NULL,
	Name NVARCHAR(200) NOT NULL,
	Price NUMERIC(10,2) NOT NULL,
	CONSTRAINT PK_Product PRIMARY KEY (ProductID)
);

CREATE TABLE Inventory(
	LocationID INT NOT NULL,
	ProductID INT NOT NULL,
	Amount INT CHECK (Amount >= 0) NOT NULL,
	CONSTRAINT PK_Inventory PRIMARY KEY (LocationID, ProductID),
	CONSTRAINT FK_Inventory_Location_LocationID FOREIGN KEY (LocationID)
		REFERENCES Location (LocationID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Inventory_Product_ProductID FOREIGN KEY (ProductID)
		REFERENCES Product (ProductID) ON DELETE CASCADE ON UPDATE CASCADE
);
--changing Date and time to string since I am unable to figure out how to update from C#
CREATE TABLE OrderHistory(
	OrderID INT IDENTITY(1, 1) NOT NULL,
	CustomerID INT NOT NULL,
	LocationID INT NOT NULL,
	Date NVARCHAR(200) NOT NULL,
	Time NVARCHAR(200) NOT NULL,
	CONSTRAINT PK_OrderHistory PRIMARY KEY (OrderID), 
	CONSTRAINT FK_OrderHistory_Customer_CustomerID FOREIGN KEY (CustomerID)
		REFERENCES Customer (CustomerID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_OrderHistory_Location_LocationID FOREIGN KEY (LocationID)
		REFERENCES Location (LocationID) ON DELETE CASCADE ON UPDATE CASCADE
);

--I could not call Orders Order as intended since it is a keyword
CREATE TABLE Orders (
	OrderID INT NOT NULL,
	ProductID INT NOT NULL,
	Amount INT CHECK (Amount >= 0) NOT NULL,
	CONSTRAINT PK_Order PRIMARY KEY (OrderID, ProductID),
	CONSTRAINT FK_Order_OrderHistory_OrderID FOREIGN KEY (OrderID)
		REFERENCES OrderHistory (OrderID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Order_Product_ProductID FOREIGN KEY (ProductID)
		REFERENCES Product (ProductID) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO Customer (FirstName, LastName) VALUES 
		('Daniel', 'Aasa'),
		('Peter', 'Pan'),
		('Mental', 'Break'),
		('Alien', 'Mind'),
		('Heavy', 'Hitter')

INSERT INTO Product (Name, Price) VALUES
		('Golden Apple', 4),
		('Bag of Tricks', 6),
		('Hidden Card', 26),
		('Sudden Trap', 44),
		('Bean Bag', 1);

INSERT INTO Location (Name, Address) VALUES
		('Golden Spot', 'move to the end of the rainbow'),
		('Shop of Dreams', 'head North, West, South, East'),
		('Last Stop', 'go to the End of Time'),
		('Great Adventure Gift Shop', 'travel through an epic quest');


		
INSERT INTO Inventory (LocationID, ProductID, Amount) VALUES
		(1,1,34), (1,2,20), (1,3,67), (1,4,82), (1,5,12),
		(2,1,45), (2,2,22), (2,3,90), (2,5,108),
		(3,1,31), (3,2,24), (3,4,93), (3,5,8),
		(4,1,91), (4,3,72), (4,4,18), (4,5,27);

INSERT INTO OrderHistory (CustomerID, LocationID, Date, Time) VALUES
		(1, 3, '08-12-2020', '13:30'),
		(3, 4, '06-6-2026', '01:30'),
		(2, 2, '01-11-1111', '17:30'),
		(4, 1, '04-12-2420', '12:59'),
		(5, 3, '09-19-9999', '02:34');

INSERT INTO Orders (OrderID, ProductID, Amount) VALUES
		(1, 1, 2),(1, 2, 3),(1, 4, 7),(1, 5, 4),
		(2, 1, 6),(2, 3, 8),(2, 4, 15),(2, 5, 9),
		(3, 1, 2),(3, 2, 3),(3, 3, 4),(3, 5, 5),
		(4, 1, 12),(4, 2, 14),(4, 3, 15),(4, 4, 5),(4, 5, 8),
		(5, 1, 1),(5, 2, 55),(5, 4, 64),(5, 5, 44);

--UPDATE Location
--SET Address = 'move to the end of the rainbow'
--WHERE LocationID = 1;

--SELECT *
--FROM Location

--DELETE FROM OrderHistory

--DROP TABLE Customer
--DROP TABLE Location
--DROP TABLE OrderHistory
--DROP TABLE Product
--DROP TABLE Orders
--DROP TABLE Inventory

--select * FROM Orders

--SELECT *FROM Inventory

--SELECT *FROM OrderHistory


--DELETE FROM OrderHistory
--WHERE OrderId = 6;


