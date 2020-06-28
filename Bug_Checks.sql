SELECT *
FROM OrderHistory

DELETE FROM OrderHistory
WHERE OrderID = 9;

SELECT*
FROM Orders

SELECT *
FROM Inventory

DBCC CHECKIDENT (OrderHistory, RESEED,1);
DBCC CHECKIDENT ( OrderHistory, RESEED );