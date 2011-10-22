CREATE TABLE [dbo].[FacetValue]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	FacetID int NOT NULL,
	[Value] nvarchar(50) NOT NULL
)
