/******************************************************************************
**		Name: MealDetails
*******************************************************************************/

EXEC dbo.MealDetails_Update @MealDetailsId = 41	,	@MealId = 31	  ,	 @FoodTypeId = 615	,	@AmountFinished = 11
EXEC dbo.MealDetails_Insert @MealDetailsId = 51	,	@MealId = 45	  ,	 @FoodTypeId = 617	,   @AmountFinished = 81
EXEC dbo.MealDetails_Insert @MealDetailsId = 61	,	@MealId = 41  	  ,  @FoodTypeId = 618	,	@AmountFinished = 91

