USE okea;
GO
DROP PROCEDURE IF EXISTS TopItems;
GO

Create PROCEDURE TopItems
AS BEGIN
SELECT Distinct top (3) i.ItemId, i.ItemName, i.UnitPrice, Sum(ci.Quantity) as UnitSold, Sum(ci.Quantity*i.UnitPrice) as Revenue
FROM Item as i
INNER JOIN CartItem as ci on ci.Itemid = i.ItemId
INNER JOIN Cart as c on c.CartId = ci.CartId
WHERE c.CheckOutDate IS NOT NULL
Group By i.Itemid, i.ItemName, i.UnitPrice
Order By Revenue desc
END