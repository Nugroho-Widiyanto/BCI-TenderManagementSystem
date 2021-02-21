--database
USE [master];
GO

IF EXISTS (SELECT NULL FROM [sys].[databases] WHERE [name] = 'NWY')
BEGIN
  ALTER DATABASE [NWY] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP  DATABASE [NWY];
END;
GO

CREATE DATABASE [NWY];
GO

--table
USE [NWY];
GO

IF EXISTS (SELECT NULL FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_NAME] = 'Tenders' AND [TABLE_SCHEMA] = 'dbo')
BEGIN
  DROP TABLE [dbo].[Scores];
END;

CREATE TABLE [dbo].[Tenders]
(
  [Id]             INT IDENTITY(1, 1) NOT NULL
, [Name]           VARCHAR(64) NOT NULL
, [ContractNumber] VARCHAR(64) NOT NULL
, [ReleaseDate]    [DATE] NULL
, [ClosingDate]    [DATE] NULL
, [Description]    VARCHAR(128) NULL
--[UserId] [int] NOT NULL,
, CONSTRAINT [PK_Tenders] PRIMARY KEY CLUSTERED([Id] ASC)
WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
ON [PRIMARY];
GO

--stored procedure
IF Object_Id('[dbo].[Usp_GetTender]', 'P') IS NOT NULL
BEGIN
  DROP PROCEDURE [dbo].[Usp_GetTender]
END;
GO

CREATE PROCEDURE [dbo].[Usp_GetTender](@id INT)
AS
BEGIN
  SELECT 
         [a].[Id]
       , [a].[Name]
       , [a].[ContractNumber]
       , [a].[ReleaseDate]
       , [a].[ClosingDate]
       , [a].[Description]
    FROM [dbo].[Tenders] AS [a]
   WHERE [Id] = @id;
END;
GO

IF Object_Id('[dbo].[Usp_DeleteTender]', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE [dbo].[Usp_DeleteTender]
END;
GO

CREATE PROCEDURE [dbo].[Usp_DeleteTender](@id INT)
AS
BEGIN
  DELETE FROM [dbo].[Tenders]
   WHERE [Id] = @id;

  SELECT @@rowCount;
END;
GO