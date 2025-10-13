UPDATE tblUser
SET 
 Password = '@_Password'
,IsReset = '1'
WHERE UserID = @_UserID
