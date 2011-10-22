ALTER TABLE [dbo].[FacetValue]
	ADD CONSTRAINT [FK_FacetValue_Facet] 
	FOREIGN KEY (FacetID)
	REFERENCES Facet (ID)	

