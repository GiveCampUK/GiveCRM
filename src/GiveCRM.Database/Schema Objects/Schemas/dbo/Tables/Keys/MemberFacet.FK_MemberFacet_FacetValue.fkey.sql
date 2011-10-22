ALTER TABLE [dbo].[MemberFacet] WITH NOCHECK
	ADD CONSTRAINT [FK_MemberFacet_FacetValue] 
	FOREIGN KEY (FacetValueID)
	REFERENCES FacetValue (ID)	

