UPDATE tblClient
SET 
 TitleID = @_TitleID
,LastName = '@_LastName'
,FirstName = '@_FirstName'
,MiddleName = '@_MiddleName'
,SuffixID = @_SuffixID
,GenderID = @_GenderID
,CivilStatusID = @_CivilStatusID
,Age = @_Age
,DateOfBirth = '@_BirthDate'
,Company = '@_Company'
,ProvinceID = @_ProvinceID
,CityID = @_CityID
,BarangayID = @_BrgyID
,DateTimeModified = GETDATE()
WHERE ClientID = @_ClientID
