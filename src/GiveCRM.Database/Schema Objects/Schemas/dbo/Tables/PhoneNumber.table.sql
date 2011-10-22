CREATE TABLE [dbo].[PhoneNumber]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	MemberID int NOT NULL,
	PhoneNumberType int NOT NULL,
	Number nvarchar(20) NOT NULL
)
