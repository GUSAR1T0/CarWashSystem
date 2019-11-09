CREATE TABLE [authentication].[Client] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)    NOT NULL,
    [LastName]    NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Client_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);