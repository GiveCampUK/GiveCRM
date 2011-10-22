ALTER TABLE [dbo].[Donation] WITH NOCHECK
	ADD CONSTRAINT [FK_Donation_Campaign] 
	FOREIGN KEY (CampaignID)
	REFERENCES Campaign (ID)	

