UPDATE tblUserPrivilege
SET
 IsAllowed = '@_IsAllowed'
WHERE
UserID = @_UserID and
PrivilegeID = @_PrivilegeID