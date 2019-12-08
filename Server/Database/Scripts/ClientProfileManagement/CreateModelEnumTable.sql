CREATE TABLE [client].[ModelEnum] (
    [Id]        INT             NOT NULL,
    [BrandId]   SMALLINT        NOT NULL,
    [Name]      NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_ModelEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModelEnum_Brand] FOREIGN KEY ([BrandId]) REFERENCES [client].[BrandEnum] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_ModelEnum_BrandAndName] UNIQUE ([BrandId], [Name])
);

INSERT INTO [client].[ModelEnum] ([Id], [BrandId], [Name])
SELECT 1, 1, N'Q7'
UNION ALL
SELECT 2, 1, N'TT'
UNION ALL
SELECT 3, 2, N'X6'
UNION ALL
SELECT 4, 2, N'M3'