CREATE TABLE [company].[CarWashService] (
    [Id]            INT               IDENTITY (1, 1) NOT NULL,
    [CarWashId]     INT               NOT NULL,
    [ServiceName]   NVARCHAR (50)     NOT NULL,
    [Description]   NVARCHAR (1024)   NULL,
    [Price]         MONEY             NOT NULL,
    [Duration]      TIME (0)          NOT NULL,
    [IsAvailable]   BIT               DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CarWashService_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CarWashService_CarWash] FOREIGN KEY ([CarWashId]) REFERENCES [company].[CarWash] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_CarWashService_CarWashAndServiceName] UNIQUE ([CarWashId], [ServiceName])
);