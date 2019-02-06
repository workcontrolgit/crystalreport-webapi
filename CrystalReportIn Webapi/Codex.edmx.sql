
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/05/2019 22:16:14
-- Generated from EDMX file: C:\apps\crystalreport\CrystalReport-in-WebApi\CrystalReportIn Webapi\Codex.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeX];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbl_Registration]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Registration];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbl_Registration'
CREATE TABLE [dbo].[tbl_Registration] (
    [Email] nvarchar(250)  NOT NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [Password] nvarchar(50)  NULL,
    [Country] nvarchar(100)  NULL,
    [Updates] bit  NULL,
    [status] bit  NULL,
    [UserName] nvarchar(50)  NULL,
    [ImageUrl] nvarchar(250)  NULL,
    [Mobile] nvarchar(11)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Email] in table 'tbl_Registration'
ALTER TABLE [dbo].[tbl_Registration]
ADD CONSTRAINT [PK_tbl_Registration]
    PRIMARY KEY CLUSTERED ([Email] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------