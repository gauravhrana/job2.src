IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[Tuition]') 
AND                       name = N'UQ_Tuition_TuitionAmount'
)
BEGIN
PRINT 'Droping UQ_Tuition_TuitionAmount'                       
ALTER TABLE dbo.Tuition
            DROP CONSTRAINT UQ_Tuition_TuitionAmount
END
GO


ALTER TABLE dbo.Tuition
ADD CONSTRAINT UQ_Tuition_TuitionAmount UNIQUE NONCLUSTERED 
(
TuitionAmount                 
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
