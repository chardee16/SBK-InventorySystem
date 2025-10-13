UPDATE tblUser
SET
 Username = '@_Username'
,Firstname = '@_Firstname'
,Middlename = '@_Middlename'
,Lastname = '@_Lastname'
WHERE
UserID = @_UserID