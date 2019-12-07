CREATE TABLE [appointment].[Appointment] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [ClientId]    INT         NULL,
    [CarWashId]   INT         NOT NULL,
    [StartTime]   DATETIME2   NOT NULL,
    [StatusId]    TINYINT     NOT NULL,
    CONSTRAINT [PK_Appointment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointment_Client] FOREIGN KEY ([ClientId]) REFERENCES [client].[Client] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [FK_Appointment_CarWash] FOREIGN KEY ([CarWashId]) REFERENCES [company].[CarWash] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointment_Status] FOREIGN KEY ([StatusId]) REFERENCES [appointment].[AppointmentStatusEnum] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [UQ_Appointment_ClientAndStartTime] UNIQUE ([ClientId], [StartTime]),
    CONSTRAINT [UQ_Appointment_CarWashAndStartTime] UNIQUE ([CarWashId], [StartTime])
);