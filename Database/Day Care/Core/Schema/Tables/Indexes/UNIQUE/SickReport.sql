IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[SickReport]') 
AND                       name = N'UQ_SickReport_TypeOfSickness'
)
BEGIN
PRINT 'Droping UQ_SickReport_TypeOfSickness'                       
ALTER TABLE dbo.SickReport
            DROP CONSTRAINT UQ_SickReport_TypeOfSickness
END
GO


ALTER TABLE dbo.SickReport
ADD CONSTRAINT UQ_SickReport_TypeOfSickness UNIQUE NONCLUSTERED 
(
TypeOfSickness                
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
