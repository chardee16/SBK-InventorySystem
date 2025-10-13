DECLARE 
@id bigint
;
SET @id = (SELECT COALESCE(MAX(GenericID),0) + 1 FROM tblGeneric)
;



INSERT INTO tblGeneric
(
	GenericID,
	GenericName
)
VALUES
(
	@id,
	'@_GenericName'
)