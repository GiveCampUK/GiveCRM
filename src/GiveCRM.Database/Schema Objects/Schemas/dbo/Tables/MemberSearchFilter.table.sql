CREATE TABLE [dbo].[MemberSearchFilter]
(
    ID int identity(1,1) not null primary key,
    CampaignId int not null,

    InternalName varchar(50) not null,
    DisplayName varchar(50) not null,
    FilterType int not null,
    SearchOperator int not null,
    Value varchar(50) not null
)
 