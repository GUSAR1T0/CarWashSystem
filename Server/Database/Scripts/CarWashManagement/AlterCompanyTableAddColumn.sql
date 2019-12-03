IF NOT EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE
          Name = N'Phone' AND
          Object_ID = Object_ID(N'company.Company')
)
BEGIN

    ALTER TABLE [company].[Company] ADD [Phone] NVARCHAR (32) NULL;

END