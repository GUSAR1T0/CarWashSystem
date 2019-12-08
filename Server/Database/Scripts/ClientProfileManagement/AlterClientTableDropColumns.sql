IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE
            Name = N'Birthday' AND
            Object_ID = Object_ID(N'client.Client')
)
BEGIN

    ALTER TABLE [client].[Client] DROP COLUMN [Birthday];

END;

IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE
          Name = N'Phone' AND
          Object_ID = Object_ID(N'client.Client')
)
BEGIN

    ALTER TABLE [client].[Client] DROP COLUMN [Phone];

END