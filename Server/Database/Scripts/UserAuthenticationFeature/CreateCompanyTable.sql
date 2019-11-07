CREATE TABLE [authentication].[Company] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)    NOT NULL,
    [Email]       NVARCHAR (255)   NOT NULL,
    [Password]    NVARCHAR (255)   NULL,
    [IsActive]    BIT              DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Company_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Company_Email] UNIQUE ([Email])
);