SELECT 
	item.ItemCode
	,item.ItemName
	,item.ItemDescription
	,generic.GenericID
	,generic.GenericName
	,item.CategoryID
	,cat.CategoryDescription
	,shelf.ShelfID
	,shelf.ShelfDescription
	,item.PurchasePrice
	,item.Price
	,item.MarkupValue
	,COALESCE(unit.UnitAbbr,'') as UnitAbbr
	,COALESCE(item.Value,0) as 'Value'
	,item.UnitID
	,COALESCE(
		(SELECT TOP 1 convert(varchar(10), ExpiryDate, 120) FROM tblTransactionDetails td
			WHERE td.ItemCode = item.ItemCode
			and td.TransactionCode IN (1,2)
			group by ExpiryDate
			having COALESCE(SUM(Quantity),0) > 0
			order by ExpiryDate)
	,'No value') as ExpiryDate
	,item.SideEffect
	,item.Barcode
	,supplier.SupplierID
	,supplier.SupplierDescription
	,COALESCE
	(
		(SELECT COALESCE(SUM(Quantity),0) FROM tblTransactionDetails td 
			WHERE td.ItemCode = item.ItemCode)
	,0) as Stock
	,COALESCE(cd.Discount, 0) AS DiscountPrice
FROM tblItems item
INNER JOIN tblCategory cat
	ON cat.CategoryID = item.CategoryID
LEFT JOIN tblGeneric generic
	ON generic.GenericID = item.ItemGenericID
INNER JOIN tblSupplier supplier
	ON supplier.SupplierID = item.SupplierID
INNER JOIN tblShelves shelf
	ON shelf.ShelfID = item.StoreShelve
LEFT JOIN tblUnit unit
	ON unit.UnitID = item.UnitID
LEFT JOIN tblClientDiscount cd
    ON cd.ItemCode = item.ItemCode
    AND cd.ClientID = @_ClientID;






