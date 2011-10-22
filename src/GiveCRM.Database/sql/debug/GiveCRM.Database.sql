/*
Deployment script for GiveCRM
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "GiveCRM"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'You cannot deploy this update script to target MARKRENDLEB333. The database for which this script was built, GiveCRM, does not exist on this server.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'MARKRENDLEB333')
BEGIN
    RAISERROR(N'The server name in the build script %s does not match the name of the target server %s. Verify whether your database project settings are correct and whether your build script is up to date.', 16, 127,N'MARKRENDLEB333',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'You cannot deploy this update script because the database for which it was built, %s , is set to READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)]
GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Creating [dbo].[Campaign]...';


GO
CREATE TABLE [dbo].[Campaign] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [RunOn]       DATE           NULL,
    [IsClosed]    CHAR (1)       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[CampaignRun]...';


GO
CREATE TABLE [dbo].[CampaignRun] (
    [CampaignID] INT NOT NULL,
    [MemberID]   INT NOT NULL
);


GO
PRINT N'Creating [dbo].[CampaignRun].[IX_CampaignRun_CampaignID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CampaignRun_CampaignID]
    ON [dbo].[CampaignRun]([CampaignID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[CampaignRun].[IX_CampaignRun_MemberID]...';


GO
CREATE NONCLUSTERED INDEX [IX_CampaignRun_MemberID]
    ON [dbo].[CampaignRun]([MemberID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Donation]...';


GO
CREATE TABLE [dbo].[Donation] (
    [ID]         INT             IDENTITY (1, 1) NOT NULL,
    [MemberID]   INT             NOT NULL,
    [Date]       DATE            NOT NULL,
    [Amount]     DECIMAL (18, 4) NOT NULL,
    [CampaignID] INT             NULL
);


GO
PRINT N'Creating [dbo].[Donation].[IX_Donation_CampaignID]...';


GO
CREATE NONCLUSTERED INDEX [IX_Donation_CampaignID]
    ON [dbo].[Donation]([CampaignID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Donation].[IX_Donation_MemberID]...';


GO
CREATE NONCLUSTERED INDEX [IX_Donation_MemberID]
    ON [dbo].[Donation]([MemberID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Facet]...';


GO
CREATE TABLE [dbo].[Facet] (
    [ID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [Type] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[FacetValue]...';


GO
CREATE TABLE [dbo].[FacetValue] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [FacetID] INT           NOT NULL,
    [Value]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[FacetValue].[IX_FacetValue_FacetID]...';


GO
CREATE NONCLUSTERED INDEX [IX_FacetValue_FacetID]
    ON [dbo].[FacetValue]([FacetID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[Member]...';


GO
CREATE TABLE [dbo].[Member] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Reference]    NVARCHAR (20) NOT NULL,
    [Title]        NVARCHAR (20) NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [Salutation]   NVARCHAR (50) NOT NULL,
    [EmailAddress] NVARCHAR (50) NULL,
    [AddressLine1] NVARCHAR (50) NULL,
    [AddressLine2] NVARCHAR (50) NULL,
    [Town]         NVARCHAR (50) NULL,
    [Region]       NVARCHAR (50) NULL,
    [PostalCode]   NVARCHAR (50) NULL,
    [Country]      NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[MemberFacet]...';


GO
CREATE TABLE [dbo].[MemberFacet] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [MemberID]      INT           NOT NULL,
    [FacetID]       INT           NOT NULL,
    [FreeTextValue] NVARCHAR (50) NULL,
    [FacetValueID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[MemberFacet].[IX_MemberFacet_FacetID]...';


GO
CREATE NONCLUSTERED INDEX [IX_MemberFacet_FacetID]
    ON [dbo].[MemberFacet]([FacetID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[MemberFacet].[IX_MemberFacet_FacetValueID]...';


GO
CREATE NONCLUSTERED INDEX [IX_MemberFacet_FacetValueID]
    ON [dbo].[MemberFacet]([FacetValueID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[MemberFacet].[IX_MemberFacet_MemberID]...';


GO
CREATE NONCLUSTERED INDEX [IX_MemberFacet_MemberID]
    ON [dbo].[MemberFacet]([MemberID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating [dbo].[PhoneNumber]...';


GO
CREATE TABLE [dbo].[PhoneNumber] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [MemberID] INT           NOT NULL,
    [Type]     NVARCHAR (15) NOT NULL,
    [Number]   NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[PhoneNumber].[IX_PhoneNumber_MemberID]...';


GO
CREATE NONCLUSTERED INDEX [IX_PhoneNumber_MemberID]
    ON [dbo].[PhoneNumber]([MemberID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0);


GO
PRINT N'Creating On column: IsClosed...';


GO
ALTER TABLE [dbo].[Campaign]
    ADD DEFAULT 'N' FOR [IsClosed];


GO
PRINT N'Creating FK_CampaignRun_Campaign...';


GO
ALTER TABLE [dbo].[CampaignRun] WITH NOCHECK
    ADD CONSTRAINT [FK_CampaignRun_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_CampaignRun_Member...';


GO
ALTER TABLE [dbo].[CampaignRun] WITH NOCHECK
    ADD CONSTRAINT [FK_CampaignRun_Member] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Donation_Campaign...';


GO
ALTER TABLE [dbo].[Donation] WITH NOCHECK
    ADD CONSTRAINT [FK_Donation_Campaign] FOREIGN KEY ([CampaignID]) REFERENCES [dbo].[Campaign] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_Donation_Member...';


GO
ALTER TABLE [dbo].[Donation] WITH NOCHECK
    ADD CONSTRAINT [FK_Donation_Member] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_FacetValue_Facet...';


GO
ALTER TABLE [dbo].[FacetValue] WITH NOCHECK
    ADD CONSTRAINT [FK_FacetValue_Facet] FOREIGN KEY ([FacetID]) REFERENCES [dbo].[Facet] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_MemberFacet_Facet...';


GO
ALTER TABLE [dbo].[MemberFacet] WITH NOCHECK
    ADD CONSTRAINT [FK_MemberFacet_Facet] FOREIGN KEY ([FacetID]) REFERENCES [dbo].[Facet] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_MemberFacet_FacetValue...';


GO
ALTER TABLE [dbo].[MemberFacet] WITH NOCHECK
    ADD CONSTRAINT [FK_MemberFacet_FacetValue] FOREIGN KEY ([FacetValueID]) REFERENCES [dbo].[FacetValue] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_MemberFacet_Member...';


GO
ALTER TABLE [dbo].[MemberFacet] WITH NOCHECK
    ADD CONSTRAINT [FK_MemberFacet_Member] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Creating FK_PhoneNumber_Member...';


GO
ALTER TABLE [dbo].[PhoneNumber] WITH NOCHECK
    ADD CONSTRAINT [FK_PhoneNumber_Member] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Member] ([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[CampaignRun] WITH CHECK CHECK CONSTRAINT [FK_CampaignRun_Campaign];

ALTER TABLE [dbo].[CampaignRun] WITH CHECK CHECK CONSTRAINT [FK_CampaignRun_Member];

ALTER TABLE [dbo].[Donation] WITH CHECK CHECK CONSTRAINT [FK_Donation_Campaign];

ALTER TABLE [dbo].[Donation] WITH CHECK CHECK CONSTRAINT [FK_Donation_Member];

ALTER TABLE [dbo].[FacetValue] WITH CHECK CHECK CONSTRAINT [FK_FacetValue_Facet];

ALTER TABLE [dbo].[MemberFacet] WITH CHECK CHECK CONSTRAINT [FK_MemberFacet_Facet];

ALTER TABLE [dbo].[MemberFacet] WITH CHECK CHECK CONSTRAINT [FK_MemberFacet_FacetValue];

ALTER TABLE [dbo].[MemberFacet] WITH CHECK CHECK CONSTRAINT [FK_MemberFacet_Member];

ALTER TABLE [dbo].[PhoneNumber] WITH CHECK CHECK CONSTRAINT [FK_PhoneNumber_Member];


GO
