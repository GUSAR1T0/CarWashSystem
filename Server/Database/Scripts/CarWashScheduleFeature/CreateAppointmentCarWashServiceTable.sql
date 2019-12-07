CREATE TABLE [appointment].[AppointmentCarWashService] (
    [Id]                 INT   IDENTITY (1, 1) NOT NULL,
    [AppointmentId]      INT   NOT NULL,
    [CarWashServiceId]   INT   NULL,
    [IsApproved]         BIT   NULL,
    CONSTRAINT [PK_AppointmentCarWashService_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AppointmentCarWashService_Appointment] FOREIGN KEY ([AppointmentId]) REFERENCES [appointment].[Appointment] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AppointmentCarWashService_CarWashService] FOREIGN KEY ([CarWashServiceId]) REFERENCES [company].[CarWashService] ([Id]) ON DELETE SET NULL,
    CONSTRAINT [UQ_AppointmentCarWashService_AppointmentAndCarWashService] UNIQUE ([AppointmentId], [CarWashServiceId])
);