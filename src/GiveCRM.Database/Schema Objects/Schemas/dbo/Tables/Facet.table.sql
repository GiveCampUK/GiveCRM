CREATE TABLE [dbo].[Facet]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	Name nvarchar(50) NOT NULL,
	[Type] nvarchar(20) NOT NULL
)
