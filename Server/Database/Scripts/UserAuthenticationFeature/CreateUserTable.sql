CREATE TABLE [authentication].[User] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Email]       NVARCHAR (255)   NOT NULL,
    [Password]    NVARCHAR (255)   NOT NULL,
    [ClientId]    INT              NULL,
    [CompanyId]   INT              NULL,
    [IsActive]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email]),
    CONSTRAINT [FK_User_Client] FOREIGN KEY ([ClientId]) REFERENCES [authentication].[Client] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_User_Company] FOREIGN KEY ([CompanyId]) REFERENCES [authentication].[Company] ([Id]) ON DELETE CASCADE
);