IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Comment'
)
ALTER TABLE Comment
ADD CONSTRAINT DF_Comment_Comment       DEFAULT ''    FOR Comment                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'StudentId'
)
ALTER TABLE Comment
ADD CONSTRAINT DF_Comment_StudentId       DEFAULT 1    FOR StudentId                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Date'
)
ALTER TABLE Comment
ADD CONSTRAINT DF_Comment_Date       DEFAULT ''    FOR Date                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'EventTypeId'
)
ALTER TABLE Comment
ADD CONSTRAINT DF_Comment_EventTypeId       DEFAULT 1   FOR EventTypeId                
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Comment%'

