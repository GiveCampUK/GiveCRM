CREATE TABLE [dbo].[ELMAH_Error] (
    [ErrorId]     UNIQUEIDENTIFIER NOT NULL,
    [Application] NVARCHAR (60)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Host]        NVARCHAR (50)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Type]        NVARCHAR (100)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Source]      NVARCHAR (60)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Message]     NVARCHAR (500)   COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [User]        NVARCHAR (50)    COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [StatusCode]  INT              NOT NULL,
    [TimeUtc]     DATETIME         NOT NULL,
    [Sequence]    INT              IDENTITY (1, 1) NOT NULL,
    [AllXml]      NTEXT            COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
);

