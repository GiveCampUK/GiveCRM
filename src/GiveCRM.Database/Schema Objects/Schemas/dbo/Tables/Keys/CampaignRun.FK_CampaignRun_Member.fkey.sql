ALTER TABLE [dbo].[CampaignRun]
	ADD CONSTRAINT [FK_CampaignRun_Member] 
	FOREIGN KEY (MemberID)
	REFERENCES Member (ID)	
