ALTER TABLE [dbo].[MemberFacet]
	ADD CONSTRAINT [FK_MemberFacet_Member] 
	FOREIGN KEY (MemberID)
	REFERENCES Member (ID)	

