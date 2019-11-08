CREATE TABLE [authentication].[User] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)    NOT NULL,
    [LastName]    NVARCHAR (50)    NOT NULL,
    [Email]       NVARCHAR (255)   NOT NULL,
    [Password]    NVARCHAR (255)   NULL,
    [IsActive]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_User_Email] UNIQUE ([Email])
);