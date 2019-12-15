CREATE TABLE [appointment].[AppointmentStatusEnum] (
    [Id]     TINYINT         NOT NULL,
    [Name]   NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_AppointmentStatusEnum_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_AppointmentStatusEnum_Name] UNIQUE ([Name])
);

INSERT INTO [appointment].[AppointmentStatusEnum] ([Id], [Name])
SELECT 1, N'Opened'
UNION ALL
SELECT 2, N'Approved'
UNION ALL
SELECT 3, N'Response Is Needed'
UNION ALL
SELECT 4, N'In Progress'
UNION ALL
SELECT 5, N'Processed'
UNION ALL
SELECT 6, N'Incident'
UNION ALL
SELECT 7, N'Closed'
UNION ALL
SELECT 8, N'Cancelled By Client'
UNION ALL
SELECT 9, N'Cancelled By Car Wash'
