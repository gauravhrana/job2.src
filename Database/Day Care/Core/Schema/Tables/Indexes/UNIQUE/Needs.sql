IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[Needs]') 
AND                       name = N'UQ_Needs_NeedItemStatus'
)
BEGIN
PRINT 'Droping UQ_Needs_NeedItemStatus'                       
ALTER TABLE dbo.Needs
            DROP CONSTRAINT UQ_Needs_NeedItemStatus
END
GO


ALTER TABLE dbo.Needs
ADD CONSTRAINT UQ_Needs_NeedItemStatus UNIQUE NONCLUSTERED 
(
NeedItemStatus                 
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
