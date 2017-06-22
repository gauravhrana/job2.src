IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'LastName'
)
ALTER TABLE Student
	ADD CONSTRAINT DF_Student_LastName		DEFAULT ''    FOR LastName                
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'FirstName'
)
ALTER TABLE Student
	ADD CONSTRAINT DF_Student_FirstName		DEFAULT ''   FOR FirstName  
GO

-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc           = 'DEFAULT_CONSTRAINT' 
AND		name   LIKE 'DF_Student%'


