SELECT
item.ItemDescription,
cat.CategoryDescription,
item.CategoryID,
ISNULL
	(
		(SELECT TOP(1) TransactionDate FROM tblTransactionDetails td 
			WHERE td.ItemCode = item.ItemCode)
	,'1900-01-01') as TransactionDate,
COALESCE
	(
		(SELECT COALESCE(SUM(Quantity),0) FROM tblTransactionDetails td 
			WHERE td.ItemCode = item.ItemCode)
	,0) as Quantity
FROM tblItems item
LEFT JOIN tblCategory cat
ON cat.CategoryID = item.CategoryID
GROUP BY item.ItemCode,
cat.CategoryDescription,
item.ItemDescription,
item.CategoryID,
item.PurchasePrice


