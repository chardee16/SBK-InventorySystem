SELECT * FROM tblUser
WHERE UserName = '@_username'
and Password = '@_password'
and IsActive = 1;