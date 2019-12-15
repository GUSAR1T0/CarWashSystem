CREATE TABLE [client].[Car] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [ClientId]          INT            NOT NULL,
    [ModelId]           INT            NOT NULL,
    [GovernmentPlate]   NVARCHAR (9)   NOT NULL,
    CONSTRAINT [PK_Car_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Car_Client] FOREIGN KEY ([ClientId]) REFERENCES [client].[Client] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Car_Model] FOREIGN KEY ([ModelId]) REFERENCES [client].[CarBrandModelEnum] ([Id]) ON DELETE CASCADE
);