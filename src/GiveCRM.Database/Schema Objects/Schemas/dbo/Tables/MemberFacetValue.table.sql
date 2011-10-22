CREATE TABLE [dbo].[MemberFacetValue]
(
	ID int IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	MemberFacetID int NOT NULL,
	FacetValueID int NOT NULL
)
