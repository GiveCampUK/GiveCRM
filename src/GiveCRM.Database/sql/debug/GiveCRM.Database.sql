/*
Deployment script for GiveCRM.Database
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "GiveCRM.Database"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'The state of the target database, %s, is not set to ONLINE. To deploy to this database, its state must be set to ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 100;


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
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


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
-- Refactoring step to update target server with deployed transaction logs
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
