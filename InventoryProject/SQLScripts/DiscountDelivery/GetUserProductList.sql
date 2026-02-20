SELECT
    td.ItemCode,
    i.ItemDescription,
    i.Price,
    u.UnitAbbr,
    u.id AS UnitID,
    COALESCE(cd.Discount, 0) AS Discount,
    SUM(CASE WHEN td.TransactionCode = 5 THEN td.Quantity ELSE 0 END) AS 'Out for Delivery',
    
    SUM(CASE WHEN td.TransactionCode = 5 THEN td.Quantity ELSE 0 END)
    -
    SUM(CASE WHEN td.TransactionCode = 6 THEN td.Quantity * -1 ELSE 0 END)
    AS Remaining

FROM tbltransactiondelivery td

INNER JOIN tblitems i 
    ON i.ItemCode = td.ItemCode

INNER JOIN tblunit u 
    ON u.id = i.UnitID    

LEFT JOIN tblclientdiscount cd 
    ON cd.ItemCode = i.ItemCode
   AND cd.ClientID = @_ClientID   

WHERE
    td.DeliveryID = @_UserID

GROUP BY 
    td.ItemCode,
    i.ItemDescription,
    i.Price,
    u.UnitAbbr,
    u.id,
    cd.Discount;