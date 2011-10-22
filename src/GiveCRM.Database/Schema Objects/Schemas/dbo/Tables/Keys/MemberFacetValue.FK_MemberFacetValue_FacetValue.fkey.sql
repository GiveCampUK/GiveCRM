ALTER TABLE [dbo].[MemberFacetValue]
	ADD CONSTRAINT [FK_MemberFacetValue_FacetValue] 
	FOREIGN KEY (FacetValueID)
	REFERENCES FacetValue (ID)	

