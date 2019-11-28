CREATE TABLE [company].[Company] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [UserId]      INT             NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_Company_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Company_User] FOREIGN KEY ([UserId]) REFERENCES [authentication].[User] ([Id]) ON DELETE CASCADE
);