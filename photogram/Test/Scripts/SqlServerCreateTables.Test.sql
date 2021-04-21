/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */
 
 
 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\SourceCode\DataBase\'


USE [master]


/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'photogram_test')
DROP DATABASE [photogram_test]



/* DataBase Creation */

DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [photogram_test] 
    ON PRIMARY ( NAME = photogram_test, FILENAME = "' + @Default_DB_Path + N'photogram_test.mdf")
    LOG ON ( NAME = photogram_test_log, FILENAME = "' + @Default_DB_Path + N'photogram_test_log.ldf")'

EXEC(@sql)
PRINT N'Database photogram_test created.'
PRINT N'Done'
GO

USE [photogram_test]

/* Drop Table Follow if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow]') 
AND type in ('U')) DROP TABLE [Follow]
GO

/* Drop Table Comment if already exists */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') 
AND type in ('U')) DROP TABLE [Comment]
GO

/* Drop Table Like if already exists */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Likes]') 
AND type in ('U')) DROP TABLE [Likes]
GO

/* Drop Table Image if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Image]') 
AND type in ('U')) BEGIN
	DROP TABLE [Image]
END
GO

/* Drop Table UserAccount if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserAccount]') 
AND type in ('U')) 
BEGIN
DROP TABLE [UserAccount]
END
GO

/* Drop Table Category if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE [Category]
GO

/*
 * Create tables.
 * Category, UserAccount, Image, Follow, LikesComment tables are created. Indexes required for the 
 * most common operations are also defined.
 */

/* Category */

CREATE TABLE Category(
	categoryId bigint IDENTITY(1,1) NOT NULL,
	name varchar(60) NOT NULL,
	CONSTRAINT [PK_categoryId] PRIMARY KEY (categoryId ASC)
)

CREATE NONCLUSTERED INDEX [IX_CategoryIndexByCategoryId] 
ON Category (categoryId ASC)

PRINT N'Table Category created.'
GO
/* User */

CREATE TABLE UserAccount(
	userId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	password varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserAccount] PRIMARY KEY (userId ASC),
	CONSTRAINT [UniqueKey_loginName] UNIQUE (loginName)
)

CREATE NONCLUSTERED INDEX [IX_UserIndexByUserId] 
ON UserAccount (userId ASC)


PRINT N'Table UserAccount created.'
GO

/* Image */

CREATE TABLE Image(
	imageId bigint IDENTITY(1,1) NOT NULL,
	title varchar(60) NOT NULL,
	description varchar(60) NOT NULL,
	date datetime2 NOT NULL,
	exifInfo varchar(60),
	categoryId bigint NOT NULL,
	userId bigint,

	CONSTRAINT [PK_ImageId] PRIMARY KEY (imageId ASC),
    CONSTRAINT [FK_ImageUserId] FOREIGN KEY(userId)
        REFERENCES UserAccount (userId) ON DELETE CASCADE,
    CONSTRAINT [FK_ImageCategoryId] FOREIGN KEY(categoryId)
        REFERENCES Category(categoryId) ON DELETE CASCADE

)

CREATE NONCLUSTERED INDEX [IX_ImageIndexByImageId] 
ON Image (imageId ASC)

PRINT N'Table Image created.'
GO

/* Follow */

CREATE TABLE Follow(
	followerId bigint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_Following] PRIMARY KEY (followerId,userId),
    CONSTRAINT [FK_FollowFollowerId] FOREIGN KEY(followerId)
        REFERENCES UserAccount(userId),
    CONSTRAINT [FK_FollowUserId] FOREIGN KEY(userId)
        REFERENCES UserAccount(userId) 
)

CREATE NONCLUSTERED INDEX IX_FollowIndexByFollowerId 
ON Follow (followerId ASC, userId ASC)

CREATE NONCLUSTERED INDEX IX_FollowIndexByUserId 
ON Follow (userId ASC,followerId ASC)

PRINT N'Table Follow created.'
GO

/* Likes */

CREATE TABLE Likes(
	imageId bigint NOT NULL,
	userId bigint NOT NULL,

	CONSTRAINT [PK_Likes] PRIMARY KEY (imageId,userId),
    CONSTRAINT [FK_LikesUserId] FOREIGN KEY(userId)
        REFERENCES UserAccount(userId),
    CONSTRAINT [FK_LikesImageId] FOREIGN KEY(imageId)
        REFERENCES Image (imageId) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_LikeIndexByImageId] 
ON Likes (imageId ASC, userId ASC)

PRINT N'Table Likes created.'
GO

/* Comment */

CREATE TABLE Comment(
	imageId bigint NOT NULL,
	userId bigint NOT NULL,
	comment varchar (255),
	date datetime2 NOT NULL,


	CONSTRAINT [PK_Coment] PRIMARY KEY (imageId,userId),
    CONSTRAINT [FK_CommentUserId] FOREIGN KEY(userId)
        REFERENCES UserAccount(userId),
    CONSTRAINT [FK_CommentImageId] FOREIGN KEY(imageId)
        REFERENCES Image (imageId) ON DELETE CASCADE
)

CREATE NONCLUSTERED INDEX [IX_CommentIndexByDateId] 
ON Comment (date ASC)

PRINT N'Table Comment created.'
GO

create trigger T_Follows
on UserAccount
instead of delete
as
    set nocount on
    delete from Follow where followerId in (select userId from deleted) 
    delete from Follow where userId in (select userId from deleted)
GO

PRINT N'Done'
