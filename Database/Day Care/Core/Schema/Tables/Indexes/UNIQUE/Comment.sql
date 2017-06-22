IF EXISTS 
(
SELECT  * 
FROM   dbo.sysindexes 
WHERE id = OBJECT_ID(N'[dbo].[Comment]') 
AND                       name = N'UQ_Comment_Comment'
)
BEGIN
PRINT 'Droping UQ_Comment_Comment'                       
ALTER TABLE dbo.Comment
            DROP CONSTRAINT UQ_Comment_Comment
END
GO


ALTER TABLE dbo.Comment
ADD CONSTRAINT UQ_Comment_Comment UNIQUE NONCLUSTERED 
(
Comment                 
)              
GO

-- Confirmation
SELECT  id, name
FROM   dbo.sysindexes 
WHERE Name LIKE 'UQ_%'
ORDER BY name
