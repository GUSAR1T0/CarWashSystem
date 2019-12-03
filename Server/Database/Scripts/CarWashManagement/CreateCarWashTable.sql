CREATE TABLE [company].[CarWash] (
    [Id]               INT               IDENTITY (1, 1) NOT NULL,
    [CompanyId]        INT               NOT NULL,
    [Name]             NVARCHAR (50)     NOT NULL,
    [Email]            NVARCHAR (255)    NULL,
    [Phone]            NVARCHAR (32)     NULL,
    [Location]         NVARCHAR (512)    NOT NULL,
    [CoordinateX]      DECIMAL (9,6)     NOT NULL,
    [CoordinateY]      DECIMAL (9,6)     NOT NULL,
    [Description]      NVARCHAR (1024)   NULL,
    [HasCafe]          BIT               DEFAULT ((0)) NOT NULL,
    [HasRestZone]      BIT               DEFAULT ((0)) NOT NULL,
    [HasParking]       BIT               DEFAULT ((0)) NOT NULL,
    [HasWC]            BIT               DEFAULT ((0)) NOT NULL,
    [HasCardPayment]   BIT               DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CarWash_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarWash_Company] FOREIGN KEY ([CompanyId]) REFERENCES [company].[Company] ([Id]) ON DELETE CASCADE
);