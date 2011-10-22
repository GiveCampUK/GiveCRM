CREATE TABLE [dbo].[PhoneNumber]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	MemberID int NOT NULL,
	[Type] nvarchar(15) NOT NULL,
	Number nvarchar(20) NOT NULL
)
