IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'Description'
)
ALTER TABLE AccidentPlace
	ADD CONSTRAINT DF_AccidentPlace_Description		DEFAULT ''    FOR Description                
GO

IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc		= 'DEFAULT_CONSTRAINT' 
    AND		name			= 'SortOrder'
)
ALTER TABLE AccidentPlace
	ADD CONSTRAINT DF_AccidentPlace_SortOrder		DEFAULT 1000   FOR SortOrder  
GO

-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc   = 'DEFAULT_CONSTRAINT' 
AND		name		LIKE 'DF_AccidentPlace%'

