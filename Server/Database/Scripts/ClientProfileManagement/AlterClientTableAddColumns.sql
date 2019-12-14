IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE
            Name = N'Birthday' AND
            Object_ID = Object_ID(N'client.Client')
)
BEGIN

    ALTER TABLE [client].[Client] ADD [Birthday] DATE NULL;

END;

IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE
          Name = N'Phone' AND
          Object_ID = Object_ID(N'client.Client')
)
BEGIN

    ALTER TABLE [client].[Client] ADD [Phone] NVARCHAR (32) NULL;

END