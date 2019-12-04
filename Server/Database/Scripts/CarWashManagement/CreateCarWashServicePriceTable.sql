CREATE TABLE [company].[CarWashServicePrice] (
    [Id]            INT               IDENTITY (1, 1) NOT NULL,
    [CarWashId]     INT               NOT NULL,
    [ServiceName]   NVARCHAR (50)     NOT NULL,
    [Description]   NVARCHAR (1024)   NULL,
    [Price]         DECIMAL (6,2)     NOT NULL,
    [Duration]      TIME (0)          NOT NULL,
    [IsAvailable]   BIT               DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CarWashServicePrice_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarWashServicePrice_CarWash] FOREIGN KEY ([CarWashId]) REFERENCES [company].[CarWash] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_CarWashServicePrice_CarWashAndServiceName] UNIQUE ([CarWashId], [ServiceName])
);