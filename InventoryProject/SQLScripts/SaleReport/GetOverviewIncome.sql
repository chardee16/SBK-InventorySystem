SELECT
	(((COALESCE(SUM(Amt), 0) + COALESCE(SUM(td.DiscountAmount), 0)) - 
	(item.PurchasePrice* COALESCE(SUM(td.Quantity) * -1, 0))) - COALESCE(SUM(td.DiscountAmount), 0))  as Income
	INTO #total
FROM tblTransactionDetails td
INNER JOIN tblItems item
	ON item.ItemCode = td.ItemCode
WHERE td.TransactionCode = 3
and YEAR(td.TransactionDate) = 2021
GROUP BY item.PurchasePrice

SELECT
	(((COALESCE(SUM(Amt), 0) + COALESCE(SUM(td.DiscountAmount), 0)) - 
	(item.PurchasePrice* COALESCE(SUM(td.Quantity) * -1, 0))) - COALESCE(SUM(td.DiscountAmount), 0))  as Income
	INTO #total2
FROM tblTransactionDetails td
INNER JOIN tblItems item
	ON item.ItemCode = td.ItemCode
WHERE td.TransactionCode = 3
and YEAR(td.TransactionDate) = 2020
GROUP BY item.PurchasePrice

SELECT (SELECT SUM(Income) as total FROM #total) as ThisYearIncome, (SELECT COALESCE(SUM(Income),0) as total FROM #total2) as LastYearIncome


DROP TABLE #total
DROP TABLE #total2



