IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[MealDetails]') 
AND                       name = N'UQ_MealDetails_AmtFinished'
)
BEGIN
PRINT 'Droping UQ_MealDetails_AmtFinished'                       
ALTER TABLE dbo.MealDetails
            DROP CONSTRAINT UQ_MealDetails_AmtFinished
END
GO


ALTER TABLE dbo.MealDetails
ADD CONSTRAINT UQ_MealDetails_AmtFinished UNIQUE NONCLUSTERED 
(
AmtFinished                 
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
