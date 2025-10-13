DECLARE @CLIENTIDINVENTORY varchar(50);

SET @CLIENTIDINVENTORY = '@_clientid';


IF(@CLIENTIDINVENTORY <> '')
BEGIN
	SELECT TOP 200 [ClientID]
		  ,[TitleID]
		  ,[LastName]
		  ,[MiddleName]
		  ,[FirstName]
		  ,[DateOfBirth] as 'BirthDate'
		  ,[Age]
		  ,[SuffixID]
		  ,[GenderID]
		  ,[CivilStatusID]
		  ,[Company]
		  ,[ProvinceID]
		  ,[CityID]
		  ,[BarangayID] as 'BrgyID'
		  ,[DateAdded]
		  ,[DateTimeAdded]
	  FROM [tblClient]
	  WHERE ClientID = '@_clientid'
END
ELSE
BEGIN
	SELECT TOP 200 [ClientID]
		  ,[TitleID]
		  ,[LastName]
		  ,[MiddleName]
		  ,[FirstName]
		  ,[DateOfBirth] as 'BirthDate'
		  ,[Age]
		  ,[SuffixID]
		  ,[GenderID]
		  ,[CivilStatusID]
		  ,[Company]
		  ,[ProvinceID]
		  ,[CityID]
		  ,[BarangayID] as 'BrgyID'
		  ,[DateAdded]
		  ,[DateTimeAdded] from tblClient
	WHERE (LastName+FirstName+MiddleName) Like '%@_fullname%'
END