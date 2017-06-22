IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'TypeOfSickness'
)
ALTER TABLE MealType
	ADD CONSTRAINT DF_MealType_TypeOfSickness		DEFAULT ''    FOR TypeOfSickness                
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'AmountOfSickness'
)
ALTER TABLE MealType
	ADD CONSTRAINT DF_MealType_AmountOfSickness		DEFAULT ''   FOR AmountOfSickness  
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'FreqOfSickness'
)
ALTER TABLE MealType
	ADD CONSTRAINT DF_MealType_FreqOfSickness		DEFAULT ''   FOR FreqOfSickness  
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'TeacherSickNote'
)
ALTER TABLE MealType
	ADD CONSTRAINT DF_MealType_TeacherSickNote		DEFAULT ''   FOR TeacherSickNote  
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'ReturnToSchoolDate'
)
ALTER TABLE MealType
	ADD CONSTRAINT DF_MealType_ReturnToSchoolDate		DEFAULT ''   FOR ReturnToSchoolDate 
GO

-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc           = 'DEFAULT_CONSTRAINT' 
AND		name   LIKE 'DF_MealType%'



