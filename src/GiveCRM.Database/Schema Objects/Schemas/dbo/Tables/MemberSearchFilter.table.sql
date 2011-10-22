CREATE TABLE [dbo].[MemberSearchFilter]
(
	ID int identity(1,1) not null primary key,
	CampaignId int not null,
	LeftHandSide varchar(50) not null,
	Operator varchar(50) not null,
	RightHandSide varchar(50) not null
)
