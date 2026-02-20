SELECT
    td.ItemCode,
    i.ItemDescription,
    i.Price,
    SUM(CASE WHEN td.TransactionCode IN (5,7) THEN td.Quantity ELSE 0 END) AS 'Out for Delivery',
    SUM(CASE WHEN td.TransactionCode = 6 THEN td.Quantity * -1 ELSE 0 END) AS 'Delivered',
    
	SUM(CASE WHEN td.TransactionCode = 5 THEN td.Quantity ELSE 0 END)
    -
    SUM(CASE WHEN td.TransactionCode = 6 THEN td.Quantity * -1 ELSE 0 END)
    -
    SUM(CASE WHEN td.TransactionCode = 7 THEN td.Quantity * -1 ELSE 0 END)
    AS 'Remaining'

FROM tbltransactiondelivery td
LEFT JOIN tblitems i on
	i.ItemCode = td.ItemCode
WHERE
    td.DeliveryID = @_UserID
    AND td.TransactionDate = '@_Date'
GROUP BY td.ItemCode, i.ItemDescription,i.Price;