ALTER TABLE [dbo].[PhoneNumber]
	ADD CONSTRAINT [FK_PhoneNumber_Member] 
	FOREIGN KEY (MemberID)
	REFERENCES Member (ID)	

