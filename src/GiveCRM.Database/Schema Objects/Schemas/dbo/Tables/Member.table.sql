CREATE TABLE [dbo].[Member]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	Reference nvarchar(20) NOT NULL,
	Title nvarchar(20) NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Salutation nvarchar(50) NOT NULL,
	EmailAddress nvarchar(50) NULL,
	AddressLine1 nvarchar(50) NULL,
	AddressLine2 nvarchar(50) NULL,
	City nvarchar(50) NULL,
	Region nvarchar(50) NULL,
	PostalCode nvarchar(50) NULL,
	Country nvarchar(50) NULL
)
