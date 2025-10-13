DECLARE 
@id bigint
;
SET @id = (SELECT COALESCE(MAX(SupplierID),0) + 1 FROM tblSupplier)
;



INSERT INTO tblSupplier
(
	SupplierID,
	SupplierDescription,
	SupplierAddress
)
VALUES
(
	@id
	,'@_SupplierDescription'
	,'@_SupplierAddress'
)