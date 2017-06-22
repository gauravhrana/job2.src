IF NOT EXISTS 
(
    SELECT	name 
    FROM	sys.objects 
    WHERE	type_desc       = 'DEFAULT_CONSTRAINT' 
    AND     name            = 'MealId'
)
ALTER TABLE MealDetail
	ADD CONSTRAINT DF_MealDetail_MealId		DEFAULT 1    FOR MealId                
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'FoodTypeId'
)
ALTER TABLE MealDetail
	ADD CONSTRAINT DF_MealDetail_FoodTypeId		DEFAULT 1   FOR FoodTypeId  
GO

IF NOT EXISTS 
(
    SELECT name 
    FROM   sys.objects 
    WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
    AND       name			  = 'AmtFinished'
)
ALTER TABLE MealDetail
	ADD CONSTRAINT DF_MealDetail_AmtFinished		DEFAULT 1   FOR AmtFinished  
GO

-- Confirmation
SELECT	name, * 
FROM	sys.objects 
WHERE	type_desc           = 'DEFAULT_CONSTRAINT' 
AND		name   LIKE 'DF_MealDetail%'

