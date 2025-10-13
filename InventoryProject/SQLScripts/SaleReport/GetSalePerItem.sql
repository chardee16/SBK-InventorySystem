SELECT
	td.ItemCode,
	item.ItemDescription,
	cat.CategoryDescription,
	(((COALESCE(SUM(Amt), 0) + COALESCE(SUM(DiscountAmount), 0)) - (item.PurchasePrice* COALESCE(SUM(Quantity) * -1, 0))) - COALESCE(SUM(DiscountAmount), 0))  as Income,
	COALESCE(SUM(Quantity) * -1, 0) as Quantity,
	COALESCE(SUM(DiscountAmount), 0) as DiscountAmount,
	COALESCE(SUM(Amt), 0)  as TotalAmount,
	td.TransactionDate
FROM tblTransactionDetails td
INNER JOIN tblItems item
	ON item.ItemCode = td.ItemCode
INNER JOIN tblCategory cat
	ON cat.CategoryID = item.CategoryID
WHERE td.TransactionCode = 3
and td.TransactionDate >= '@_DateStart'
and td.TransactionDate <= '@_DateEnd'
GROUP BY td.TransactionDate,
	td.ItemCode,
	cat.CategoryDescription,
	item.ItemDescription,
	item.PurchasePrice
UNION
SELECT
	0 as 'ItemCode',
	td.Explanation as 'ItemDescription',
	'Other Income' as 'CategoryDescription',
	COALESCE(ts.Price,0) as 'Income',
	COALESCE(ts.Quantity,0) as 'Quantity',
	COALESCE(ts.DiscountAmount,0) as 'DiscountAmount',
	COALESCE(ts.Amt,0) as 'TotalAmount',
	td.TransactionDate
FROM tblTransactionSummary td
INNER JOIN tblTransactionDetails ts ON td.TransactionCode = ts.TransactionCode 
and td.CTLNo = ts.CTLNo and td.TransYear = ts.TransYear
WHERE td.TransactionCode = 4
and td.TransactionDate >= '@_DateStart'
and td.TransactionDate <= '@_DateEnd'
ORDER BY td.TransactionDate