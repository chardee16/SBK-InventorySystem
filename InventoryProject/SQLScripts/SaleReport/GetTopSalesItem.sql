SELECT TOP(10) COALESCE(SUM(Quantity),0)* -1 as Sales, it.ItemDescription,
coalesce((SELECT(Sum(Quantity)) From tblTransactionDetails Where ItemCode = td.ItemCode and Quantity > 0), 0) as TotalStocks
FROM tblTransactionDetails td 
INNER JOIN tblItems it ON it.ItemCode = td.ItemCode
WHERE td.Quantity < 0 and td.TransactionCode =3
GROUP BY td.ItemCode,it.ItemDescription
ORDER BY COALESCE(SUM(Quantity),0)* -1 DESC