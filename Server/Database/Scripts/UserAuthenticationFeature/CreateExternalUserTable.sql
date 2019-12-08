CREATE TABLE [authentication].[ExternalUser] (
    [Id]                    INT                IDENTITY (1, 1) NOT NULL,
    [ExternalId]            NVARCHAR (128)     NOT NULL,
    [SchemaId]              TINYINT            NOT NULL,
    [AuthenticationToken]   UNIQUEIDENTIFIER   NULL,
    [Email]                 NVARCHAR (255)     NULL,
    CONSTRAINT [PK_ExternalUser_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExternalUser_Schema] FOREIGN KEY ([SchemaId]) REFERENCES [authentication].[ExternalUserAuthenticationSchemaEnum] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_ExternalUser_ExternalIdAndSchema] UNIQUE ([ExternalId], [SchemaId])
);