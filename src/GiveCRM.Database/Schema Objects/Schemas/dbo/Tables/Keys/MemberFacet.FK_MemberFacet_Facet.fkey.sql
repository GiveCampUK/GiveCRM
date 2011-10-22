ALTER TABLE [dbo].[MemberFacet]
	ADD CONSTRAINT [FK_MemberFacet_Facet] 
	FOREIGN KEY (FacetID)
	REFERENCES Facet (ID)	

