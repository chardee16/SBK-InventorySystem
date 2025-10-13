SELECT td.Quantity,ts.DateTimeAdded, it.ItemDescription, us.Username 
FROM tblTransactionDetails td 
LEFT JOIN tblItems it ON td.ItemCode = it.ItemCode
LEFT JOIN tblUser us ON td.EncodedBy = us.UserID
LEFT JOIN tblTransactionSummary ts ON td.CTLNo = ts.CTLNo and 
td.TransYear = ts.TransYear and td.TransactionCode = ts.TransactionCode
WHERE td.ItemCode = @_ItemCode and td.TransactionCode = 2 and Quantity > 0
ORDER BY ts.DateTimeAdded DESC
