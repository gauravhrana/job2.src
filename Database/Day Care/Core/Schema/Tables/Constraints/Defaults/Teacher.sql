IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'LastName'
)
ALTER TABLE Teacher
	ADD CONSTRAINT DF_Teacher_LastName		DEFAULT ''    FOR LastName                
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'FirstName'
)
ALTER TABLE Teacher
	ADD CONSTRAINT DF_Teacher_FirstName		DEFAULT ''   FOR FirstName  
GO

-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc           = 'DEFAULT_CONSTRAINT' 
AND		name   LIKE 'DF_Teacher%'


