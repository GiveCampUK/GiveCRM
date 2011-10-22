CREATE TABLE [dbo].[Donation]
(
	ID int identity(1,1) NOT NULL, 
	MemberID int NOT NULL,
	[Date] date NOT NULL,
	Amount decimal(18,4) NOT NULL,
	CampaignID int NULL
)
