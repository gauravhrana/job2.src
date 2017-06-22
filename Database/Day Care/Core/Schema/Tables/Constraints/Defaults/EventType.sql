﻿IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Description'
)
ALTER TABLE EventType
ADD CONSTRAINT DF_EventType_Description        DEFAULT ''    FOR Description                
GO


IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'SortOrder'
)
ALTER TABLE EventType
ADD CONSTRAINT DF_EventType_SortOrder DEFAULT 1000   FOR SortOrder  
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_EventType%'

