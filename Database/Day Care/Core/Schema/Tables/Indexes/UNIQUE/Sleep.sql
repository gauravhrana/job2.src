IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[Sleep]') 
AND                       name = N'UQ_Sleep_Date'
)
BEGIN
PRINT 'Droping UQ_Sleep_Name'                       
ALTER TABLE dbo.Sleep
            DROP CONSTRAINT UQ_Sleep_Date
END
GO


ALTER TABLE dbo.Sleep
ADD CONSTRAINT UQ_Sleep_Date UNIQUE NONCLUSTERED 
(
Date                 
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
