﻿--ALTER TABLE Chats
--DROP COLUMN Id;


--ALTER TABLE Chats
--ADD Id int identity (1,1) not null;
 
 --drop Table Chats
create Table News(
Id INT IDENTITY (1,1) NOT NULL,
Headline  NVARCHAR (MAX) NULL,
NewsText  NVARCHAR (MAX) NULL,
Created DATETIME2 (7)  NOT NULL,
PictureUrl NVARCHAR (MAX) NULL,
PicName NVARCHAR (100) NULL,
PicGuid NVARCHAR (MAX) NULL,
Author NVARCHAR (50) NULL,
Category NVARCHAR (MAX) NULL,)