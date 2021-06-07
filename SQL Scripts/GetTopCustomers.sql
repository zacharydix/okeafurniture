USE okea;
GO
DROP PROCEDURE IF EXISTS TopCustomers;
GO

Create PROCEDURE TopCustomers
AS BEGIN
SELECT top (3) 
    a.AccountId,
	a.FirstName + ' ' + a.LastName FullName,
    a.Email,
    sum(c.OrderTotal) TotalSpent
FROM Account a
INNER JOIN Cart c ON c.AccountId = a.AccountId
WHERE c.CheckOutDate IS NOT NULL
GROUP BY a.AccountId, a.FirstName, a.LastName, a.Email
ORDER BY TotalSpent DESC
END