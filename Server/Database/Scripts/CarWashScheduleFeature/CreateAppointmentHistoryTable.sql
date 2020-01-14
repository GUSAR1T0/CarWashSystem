CREATE TABLE [appointment].[AppointmentHistory] (
    [Id]              INT               IDENTITY (1, 1) NOT NULL,
    [AppointmentId]   INT               NOT NULL,
    [Action]          NVARCHAR (1024)   NOT NULL,
    [Timestamp]       DATETIME2         DEFAULT ((SYSDATETIME())) NOT NULL,
    CONSTRAINT [PK_AppointmentHistory_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AppointmentHistory_Appointment] FOREIGN KEY ([AppointmentId]) REFERENCES [appointment].[Appointment] ([Id]) ON DELETE CASCADE
);