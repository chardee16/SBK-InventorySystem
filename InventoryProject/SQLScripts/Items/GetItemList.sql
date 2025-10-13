SELECT 
	item.ItemCode
	,item.ItemName
	,item.ItemDescription
	,item.CategoryID
	,cat.CategoryDescription
	,item.Price
	,item.Barcode
	,COALESCE(unit.UnitAbbr,'') as UnitAbbr
	,item.UnitID
	,COALESCE
	(
		(SELECT COALESCE(SUM(Quantity),0) FROM tblTransactionDetails td 
			WHERE td.ItemCode = item.ItemCode)
	,0) as Stock
	,COALESCE(
		(SELECT TOP 1 convert(varchar(10), ExpiryDate, 120) FROM tblTransactionDetails td
			WHERE td.ItemCode = item.ItemCode
			and td.TransactionCode IN (1,2)
			group by ExpiryDate
			having COALESCE(SUM(Quantity),0) > 0
			order by ExpiryDate)
	,'No value') as ExpiryDate
FROM tblItems item
INNER JOIN tblCategory cat
	ON cat.CategoryID = item.CategoryID
LEFT JOIN tblUnit unit
	ON unit.UnitID = item.UnitID