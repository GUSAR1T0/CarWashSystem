CREATE TABLE [client].[Client] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      INT             NOT NULL,
    [FirstName]   NVARCHAR (50)   NOT NULL,
    [LastName]    NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Client_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Client_User] FOREIGN KEY ([UserId]) REFERENCES [authentication].[User] ([Id]) ON DELETE CASCADE
);