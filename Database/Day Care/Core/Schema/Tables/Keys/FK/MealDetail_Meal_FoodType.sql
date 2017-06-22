   ALTER TABLE dbo.MealDetail
	ADD CONSTRAINT FK_MealDetail_Meal FOREIGN KEY 
	(
		MealId
	) 
	REFERENCES Meal
	(
		MealId
	)
	GO
	ALTER TABLE dbo.MealDetail
	ADD CONSTRAINT FK_MealDetail_FoodType FOREIGN KEY 
	(
		FoodTypeId
	) 
	REFERENCES FoodType
	(
		FoodTypeId
	)
    GO
	
