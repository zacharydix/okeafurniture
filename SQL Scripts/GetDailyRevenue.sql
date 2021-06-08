USE okea;
GO
DROP PROCEDURE IF EXISTS DailyRevenue;
GO

Create PROCEDURE DailyRevenue (
    @StartDate date,
    @EndDate date
)
AS BEGIN
SELECT
    CAST(c.CheckOutDate AS Date) SaleDate,
    sum(ci.Quantity) QuantitySold,
    sum(c.OrderTotal) RevenueGenerated
FROM Cart c
INNER JOIN CartItem ci ON ci.CartId = c.CartId
WHERE c.CheckOutDate BETWEEN @StartDate AND @EndDate
GROUP BY CAST(c.CheckOutDate AS Date)
ORDER BY CAST(c.CheckOutDate AS Date) DESC
END
