ALTER TABLE [dbo].[CampaignRun]
	ADD CONSTRAINT [FK_CampaignRun_Campaign] 
	FOREIGN KEY (CampaignID)
	REFERENCES Campaign (ID)	

