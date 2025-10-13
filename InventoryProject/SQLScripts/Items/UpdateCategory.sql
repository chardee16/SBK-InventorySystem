UPDATE tblCategory
SET
CategoryDescription = '@_CategoryDescription'
WHERE CategoryID = @_CategoryID