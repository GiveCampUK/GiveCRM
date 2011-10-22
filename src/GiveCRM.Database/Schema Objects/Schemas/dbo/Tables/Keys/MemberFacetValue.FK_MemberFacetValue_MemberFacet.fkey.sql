ALTER TABLE [dbo].[MemberFacetValue]
	ADD CONSTRAINT [FK_MemberFacetValue_MemberFacet] 
	FOREIGN KEY (MemberFacetID)
	REFERENCES MemberFacet (ID)	

