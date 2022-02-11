
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/11/2022 11:02:46
-- Generated from EDMX file: C:\Users\lcabraja\source\repos\PPPK\ApartmentsManager\ApartmentsManager\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PPPK_Delivery_4];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PetUploadedFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedFiles] DROP CONSTRAINT [FK_PetUploadedFile];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pets] DROP CONSTRAINT [FK_PersonPet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Pets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pets];
GO
IF OBJECT_ID(N'[dbo].[UploadedFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UploadedFiles];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Pets'
CREATE TABLE [dbo].[Pets] (
    [IDPet] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [OwnerID] int  NOT NULL
);
GO

-- Creating table 'UploadedFiles'
CREATE TABLE [dbo].[UploadedFiles] (
    [IDUploadedFile] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [ContentType] nvarchar(20)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [PetID] int  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [IDPerson] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Age] int  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDPet] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [PK_Pets]
    PRIMARY KEY CLUSTERED ([IDPet] ASC);
GO

-- Creating primary key on [IDUploadedFile] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [PK_UploadedFiles]
    PRIMARY KEY CLUSTERED ([IDUploadedFile] ASC);
GO

-- Creating primary key on [IDPerson] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([IDPerson] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OwnerID] in table 'Pets'
ALTER TABLE [dbo].[Pets]
ADD CONSTRAINT [FK_PersonPet]
    FOREIGN KEY ([OwnerID])
    REFERENCES [dbo].[People]
        ([IDPerson])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPet'
CREATE INDEX [IX_FK_PersonPet]
ON [dbo].[Pets]
    ([OwnerID]);
GO

-- Creating foreign key on [PetID] in table 'UploadedFiles'
ALTER TABLE [dbo].[UploadedFiles]
ADD CONSTRAINT [FK_PetUploadedFile]
    FOREIGN KEY ([PetID])
    REFERENCES [dbo].[Pets]
        ([IDPet])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PetUploadedFile'
CREATE INDEX [IX_FK_PetUploadedFile]
ON [dbo].[UploadedFiles]
    ([PetID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------