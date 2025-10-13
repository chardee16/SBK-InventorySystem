
DECLARE 
@id bigint
;
SET @id = (SELECT COALESCE(MAX(ShelfID),0) + 1 FROM tblShelves)
;


INSERT INTO tblShelves
(
	ShelfID,
	ShelfDescription
)
VALUES
(
	@id,
	'@_ShelfDescription'
)