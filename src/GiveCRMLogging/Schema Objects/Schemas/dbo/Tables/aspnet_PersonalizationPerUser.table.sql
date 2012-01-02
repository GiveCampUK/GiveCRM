CREATE TABLE [dbo].[aspnet_PersonalizationPerUser] (
    [Id]              UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [PathId]          UNIQUEIDENTIFIER NULL,
    [UserId]          UNIQUEIDENTIFIER NULL,
    [PageSettings]    IMAGE            NOT NULL,
    [LastUpdatedDate] DATETIME         NOT NULL,
    PRIMARY KEY NONCLUSTERED ([Id] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY],
    FOREIGN KEY ([PathId]) REFERENCES [dbo].[aspnet_Paths] ([PathId]) ON DELETE NO ACTION ON UPDATE NO ACTION,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION
);


GO
EXECUTE sp_tableoption @TableNamePattern = N'[dbo].[aspnet_PersonalizationPerUser]', @OptionName = N'text in row', @OptionValue = N'6000';

