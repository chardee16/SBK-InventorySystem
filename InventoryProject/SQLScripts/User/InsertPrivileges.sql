INSERT INTO tblUserPrivilege
(
	[UserID]
    ,[PrivilegeID]
    ,[IsAllowed]
)
VALUES
(
  @_UserID
  ,@_PrivilegeID
  ,'@_IsAllowed'

)