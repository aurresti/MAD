
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/24/2021 21:18:04
-- Generated from EDMX file: C:\Users\USUARIO\source\repos\aurresti\MAD\photogram\Model\Photogram.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [photogram];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CommentImageId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_CommentImageId];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentUserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_CommentUserId];
GO
IF OBJECT_ID(N'[dbo].[FK_FollowFollowerId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follow] DROP CONSTRAINT [FK_FollowFollowerId];
GO
IF OBJECT_ID(N'[dbo].[FK_FollowUserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Follow] DROP CONSTRAINT [FK_FollowUserId];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageCategoryId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_ImageCategoryId];
GO
IF OBJECT_ID(N'[dbo].[FK_ImageUserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_ImageUserId];
GO
IF OBJECT_ID(N'[dbo].[FK_LikesImageId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_LikesImageId];
GO
IF OBJECT_ID(N'[dbo].[FK_LikesUserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Likes] DROP CONSTRAINT [FK_LikesUserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[Follow]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Follow];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Likes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Likes];
GO
IF OBJECT_ID(N'[dbo].[UserAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAccount];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [categoryId] bigint IDENTITY(1,1) NOT NULL,
    [name] varchar(60)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [commentId] bigint IDENTITY(1,1) NOT NULL,
    [imageId] bigint  NOT NULL,
    [userId] bigint  NOT NULL,
    [comment1] varchar(255)  NULL,
    [date] datetime  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [imageId] bigint IDENTITY(1,1) NOT NULL,
    [title] varchar(60)  NOT NULL,
    [description] varchar(60)  NOT NULL,
    [date] datetime  NOT NULL,
    [exifInfo] varchar(60)  NULL,
    [categoryId] bigint  NOT NULL,
    [userId] bigint  NULL,
    [imageView] varchar(60)  NOT NULL
);
GO

-- Creating table 'UserAccounts'
CREATE TABLE [dbo].[UserAccounts] (
    [userId] bigint IDENTITY(1,1) NOT NULL,
    [loginName] varchar(30)  NOT NULL,
    [password] varchar(50)  NOT NULL,
    [firstName] varchar(30)  NOT NULL,
    [lastName] varchar(40)  NOT NULL,
    [email] varchar(60)  NOT NULL,
    [language] varchar(2)  NULL,
    [country] varchar(2)  NULL
);
GO

-- Creating table 'Follow'
CREATE TABLE [dbo].[Follow] (
    [UserAccounts_userId] bigint  NOT NULL,
    [UserAccount1_userId] bigint  NOT NULL
);
GO

-- Creating table 'Likes'
CREATE TABLE [dbo].[Likes] (
    [Images1_imageId] bigint  NOT NULL,
    [UserAccounts_userId] bigint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [categoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([categoryId] ASC);
GO

-- Creating primary key on [commentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [imageId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([imageId] ASC);
GO

-- Creating primary key on [userId] in table 'UserAccounts'
ALTER TABLE [dbo].[UserAccounts]
ADD CONSTRAINT [PK_UserAccounts]
    PRIMARY KEY CLUSTERED ([userId] ASC);
GO

-- Creating primary key on [UserAccounts_userId], [UserAccount1_userId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [PK_Follow]
    PRIMARY KEY CLUSTERED ([UserAccounts_userId], [UserAccount1_userId] ASC);
GO

-- Creating primary key on [Images1_imageId], [UserAccounts_userId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [PK_Likes]
    PRIMARY KEY CLUSTERED ([Images1_imageId], [UserAccounts_userId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [categoryId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ImageCategoryId]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Categories]
        ([categoryId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageCategoryId'
CREATE INDEX [IX_FK_ImageCategoryId]
ON [dbo].[Images]
    ([categoryId]);
GO

-- Creating foreign key on [imageId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentImageId]
    FOREIGN KEY ([imageId])
    REFERENCES [dbo].[Images]
        ([imageId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentImageId'
CREATE INDEX [IX_FK_CommentImageId]
ON [dbo].[Comments]
    ([imageId]);
GO

-- Creating foreign key on [userId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentUserId]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[UserAccounts]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentUserId'
CREATE INDEX [IX_FK_CommentUserId]
ON [dbo].[Comments]
    ([userId]);
GO

-- Creating foreign key on [userId] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_ImageUserId]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[UserAccounts]
        ([userId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImageUserId'
CREATE INDEX [IX_FK_ImageUserId]
ON [dbo].[Images]
    ([userId]);
GO

-- Creating foreign key on [UserAccounts_userId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [FK_Follow_UserAccount]
    FOREIGN KEY ([UserAccounts_userId])
    REFERENCES [dbo].[UserAccounts]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserAccount1_userId] in table 'Follow'
ALTER TABLE [dbo].[Follow]
ADD CONSTRAINT [FK_Follow_UserAccount1]
    FOREIGN KEY ([UserAccount1_userId])
    REFERENCES [dbo].[UserAccounts]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Follow_UserAccount1'
CREATE INDEX [IX_FK_Follow_UserAccount1]
ON [dbo].[Follow]
    ([UserAccount1_userId]);
GO

-- Creating foreign key on [Images1_imageId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_Likes_Image]
    FOREIGN KEY ([Images1_imageId])
    REFERENCES [dbo].[Images]
        ([imageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserAccounts_userId] in table 'Likes'
ALTER TABLE [dbo].[Likes]
ADD CONSTRAINT [FK_Likes_UserAccount]
    FOREIGN KEY ([UserAccounts_userId])
    REFERENCES [dbo].[UserAccounts]
        ([userId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Likes_UserAccount'
CREATE INDEX [IX_FK_Likes_UserAccount]
ON [dbo].[Likes]
    ([UserAccounts_userId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------