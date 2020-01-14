CREATE TABLE [appointment].[Appointment] (
    [Id]                   INT         IDENTITY (1, 1) NOT NULL,
    [CarId]                INT         NULL,
    [CarWashId]            INT         NULL,
    [RequestedStartTime]   DATETIME2   NOT NULL,
    [ApprovedStartTime]    DATETIME2   NULL,
    [StatusId]             TINYINT     NOT NULL,
    CONSTRAINT [PK_Appointment_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Appointment_Car] FOREIGN KEY ([CarId]) REFERENCES [client].[Car] ([Id]),
    CONSTRAINT [FK_Appointment_CarWash] FOREIGN KEY ([CarWashId]) REFERENCES [company].[CarWash] ([Id]),
    CONSTRAINT [FK_Appointment_Status] FOREIGN KEY ([StatusId]) REFERENCES [appointment].[AppointmentStatusEnum] ([Id]) ON DELETE CASCADE
);