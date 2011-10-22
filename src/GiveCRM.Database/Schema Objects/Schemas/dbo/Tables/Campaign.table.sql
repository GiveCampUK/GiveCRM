CREATE TABLE [dbo].[Campaign]
(
	ID int identity(1,1) NOT NULL PRIMARY KEY, 
	[Name] nvarchar(50) NOT NULL,
	Description nvarchar(max) NULL,
	RunOn date NULL,
	IsClosed char(1) default 'N' NOT NULL
)
