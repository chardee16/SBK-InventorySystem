UPDATE tblUser
SET 
 IsActive = '@_IsActive'
WHERE UserID = @_UserID
