CREATE TABLE [authentication].[User] (
    [Id]               INT   IDENTITY (1, 1) NOT NULL,
    [InternalUserId]   INT   NULL,
    [ExternalUserId]   INT   NULL,
    [IsActivated]      BIT   DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_InternalUser] FOREIGN KEY ([InternalUserId]) REFERENCES [authentication].[InternalUser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_User_ExternalUser] FOREIGN KEY ([ExternalUserId]) REFERENCES [authentication].[ExternalUser] ([Id]) ON DELETE CASCADE
);