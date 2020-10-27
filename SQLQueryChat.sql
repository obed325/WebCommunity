CREATE TABLE [dbo].[Chats] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [chatHistory] NVARCHAR (MAX) NULL,
    [IdName]      NVARCHAR (450) NULL,
    CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED ([Id] ASC),
    
);
