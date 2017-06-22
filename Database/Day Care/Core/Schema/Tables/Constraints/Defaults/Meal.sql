IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'StudentId'
)
ALTER TABLE Meal
ADD CONSTRAINT DF_Meal_StudentId        DEFAULT 1    FOR StudentId               
GO


IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name			  = 'Date'
)
ALTER TABLE Meal
ADD CONSTRAINT DF_Meal_Date DEFAULT ''   FOR Date  
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'MealTypeId'
)
ALTER TABLE Meal
ADD CONSTRAINT DF_Meal_MealTypeId        DEFAULT 1    FOR MealTypeId               
GO


-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Meal%'

