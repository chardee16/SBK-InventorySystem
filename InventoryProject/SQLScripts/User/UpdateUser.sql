UPDATE tblUser
SET 
 Password = '@_Password'
,IsReset = '0'
WHERE UserID = @_UserID
