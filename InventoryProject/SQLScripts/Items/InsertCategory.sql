DECLARE 
@id bigint
;
SET @id = (SELECT COALESCE(MAX(CategoryID),0) + 1 FROM tblCategory)
;


INSERT INTO tblCategory
(
	CategoryID,
	CategoryDescription
)
VALUES
(
	@id,
	'@_CategoryDescription'
)