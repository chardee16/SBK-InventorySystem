UPDATE tblSupplier
SET
SupplierDescription = '@_SupplierDescription'
,SupplierAddress = '@_SupplierAddress'
WHERE SupplierID = @_SupplierID