IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'NapEnd'
)
ALTER TABLE Sleep
ADD CONSTRAINT DF_Sleep_NapEnd      DEFAULT ''    FOR NapEnd              
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'Date'
)
ALTER TABLE Sleep
ADD CONSTRAINT DF_Sleep_Date       DEFAULT ''    FOR Date                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'NapStart'
)
ALTER TABLE Sleep
ADD CONSTRAINT DF_Sleep_NapStart DEFAULT 1000   FOR NapStart  
GO

-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Sleep%'
