ALTER TABLE dbo.Meal
	ADD CONSTRAINT FK_Meal_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Meal
	ADD CONSTRAINT FK_Meal_MealType FOREIGN KEY 
	(
		MealTypeId
	) 
	REFERENCES MealType
	(
		MealTypeId
	)
GO
	
