CREATE TABLE [authentication].[ExternalClient] (
    [Id]                    INT                IDENTITY (1, 1) NOT NULL,
    [ExternalId]            NVARCHAR (128)     NULL,
    [Schema]                TINYINT            DEFAULT ((0)) NOT NULL,
    [AuthenticationToken]   UNIQUEIDENTIFIER   NULL,
    CONSTRAINT [PK_ExternalClient_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);