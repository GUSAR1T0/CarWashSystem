CREATE TABLE [client].[CarBrandEnum] (
    [Id]     SMALLINT        NOT NULL,
    [Name]   NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_CarBrandEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_CarBrandEnum_Name] UNIQUE ([Name])
);

INSERT INTO [client].[CarBrandEnum] ([Id], [Name])
SELECT 1, N'Audi'
UNION ALL
SELECT 2, N'BMW'
UNION ALL
SELECT 3, N'Mercedes-Benz'
UNION ALL
SELECT 4, N'Volkswagen'
UNION ALL
SELECT 5, N'Renault'
