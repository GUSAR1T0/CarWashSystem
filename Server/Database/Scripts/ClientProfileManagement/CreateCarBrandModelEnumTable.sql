CREATE TABLE [client].[CarBrandModelEnum] (
    [Id]        INT             NOT NULL,
    [BrandId]   SMALLINT        NOT NULL,
    [Name]      NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_CarBrandModelEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarBrandModelEnum_Brand] FOREIGN KEY ([BrandId]) REFERENCES [client].[CarBrandEnum] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_CarBrandModelEnum_BrandAndName] UNIQUE ([BrandId], [Name])
);

INSERT INTO [client].[CarBrandModelEnum] ([Id], [BrandId], [Name])
SELECT 1, 1, N'Q7'
UNION ALL
SELECT 2, 1, N'TT'
UNION ALL
SELECT 3, 2, N'X6'
UNION ALL
SELECT 4, 2, N'M3'
UNION ALL
SELECT 5, 3, N'AMG'
UNION ALL
SELECT 6, 3, N'GLA'
UNION ALL
SELECT 7, 4, N'Touareg'
UNION ALL
SELECT 8, 4, N'Polo'
UNION ALL
SELECT 9, 5, N'Sandero'
UNION ALL
SELECT 10, 5, N'Logan'