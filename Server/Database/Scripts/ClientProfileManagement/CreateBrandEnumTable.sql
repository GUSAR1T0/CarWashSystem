CREATE TABLE [client].[BrandEnum] (
    [Id]     SMALLINT        NOT NULL,
    [Name]   NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_BrandEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_BrandEnum_Name] UNIQUE ([Name])
);

INSERT INTO [client].[BrandEnum] ([Id], [Name])
SELECT 1, N'Audi'
UNION ALL
SELECT 2, N'BMW'