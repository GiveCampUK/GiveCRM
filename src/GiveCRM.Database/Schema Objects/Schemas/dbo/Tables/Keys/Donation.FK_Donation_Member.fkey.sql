ALTER TABLE [dbo].[Donation]
	ADD CONSTRAINT [FK_Donation_Member] 
	FOREIGN KEY (MemberID)
	REFERENCES Member (ID)	

