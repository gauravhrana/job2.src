
IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'StudentId'
)
ALTER TABLE Activity
ADD CONSTRAINT DF_Activity_StudentId        DEFAULT 1    FOR StudentId               
GO


IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'ActivityTypeId'
)
ALTER TABLE Activity
ADD CONSTRAINT DF_Activity_ActivityTypeId DEFAULT 1   FOR ActivityTypeId  
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'ActivitySubTypeId'
)
ALTER TABLE Activity
ADD CONSTRAINT DF_Activity_ActivitySubTypeId DEFAULT 1   FOR ActivitySubTypeId  
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Activity%'

