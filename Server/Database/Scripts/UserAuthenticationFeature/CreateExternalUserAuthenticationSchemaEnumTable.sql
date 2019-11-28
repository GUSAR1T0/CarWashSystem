CREATE TABLE [authentication].[ExternalUserAuthenticationSchemaEnum] (
    [Id]     TINYINT         NOT NULL,
    [Name]   NVARCHAR (16)   NOT NULL,
    CONSTRAINT [PK_ExternalUserAuthenticationSchemaEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_ExternalUserAuthenticationSchemaEnum_Name] UNIQUE ([Name])
);

INSERT INTO [authentication].[ExternalUserAuthenticationSchemaEnum] ([Id], [Name])
SELECT 1, N'Google'
UNION ALL
SELECT 2, N'VK'