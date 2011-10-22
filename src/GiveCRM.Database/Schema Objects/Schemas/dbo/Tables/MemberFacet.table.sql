CREATE TABLE [dbo].[MemberFacet]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY,
	MemberID int NOT NULL,
	FacetID int NOT NULL,
	FreeTextValue nvarchar(50) null,
	FacetValueID int NULL
)
