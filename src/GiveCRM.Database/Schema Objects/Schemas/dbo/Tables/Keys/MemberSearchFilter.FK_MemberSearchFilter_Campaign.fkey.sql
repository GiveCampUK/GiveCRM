ALTER TABLE [dbo].[MemberSearchFilter]
	ADD CONSTRAINT [FK_MemberSearchFilter_Campaign] 
	FOREIGN KEY (CampaignID)
	REFERENCES Campaign (ID)	

