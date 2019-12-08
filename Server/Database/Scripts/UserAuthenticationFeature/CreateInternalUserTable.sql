CREATE TABLE [authentication].[InternalUser] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [Email]      NVARCHAR (255)   NOT NULL,
    [Password]   NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_InternalUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_InternalUser_Email] UNIQUE ([Email])
);