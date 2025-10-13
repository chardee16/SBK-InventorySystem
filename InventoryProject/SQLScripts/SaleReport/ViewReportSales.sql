SELECT td.CTLNo,td.Amt,td.Price,gen.GenericName,cat.CategoryDescription, items.ItemDescription,
ISNULL(un.UnitDescription, '') as 'UnitDescription', ISNULL(td.UnitID, 0) as 'UnitID',
items.CategoryID,items.ItemGenericID,td.TransactionDate, td.Quantity * -1 as 'Quantity'
 FROM tblTransactionDetails td
LEFT JOIN tblItems items on items.ItemCode = td.ItemCode
LEFT JOIN tblGeneric gen on gen.GenericID = items.ItemGenericID
LEFT JOIN tblCategory cat on cat.CategoryID = items.CategoryID
LEFT JOIN tblUnit un on un.UnitID = td.UnitID
WHERE td.TransactionDate >= '@_DateStart' 
and td.TransactionDate <= '@_DateEnd'
and td.TransactionCode = 3
@_Name
@_Category
ORDER BY items.ItemGenericID,td.TransactionDate



