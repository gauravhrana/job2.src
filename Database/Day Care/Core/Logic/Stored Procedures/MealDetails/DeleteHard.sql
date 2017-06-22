IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MealDetailDeleteHard'
	DROP  Procedure MealDetailDeleteHard
END
GO

PRINT 'Creating Procedure MealDetailDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: MealDetailDelete
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.MealDetailDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'MealDetail'	
)
AS
BEGIN

	IF @KeyType = 'MealDetailId'
		BEGIN

			DELETE	 dbo.MealDetail
			WHERE	 MealDetailId = @KeyId	

		END
	ELSE IF @KeyType = 'MealId'
		BEGIN

			DELETE	 dbo.MealDetail
			WHERE	 MealId = @KeyId

		END
	ELSE IF @KeyType = 'FoodTypeId'
		BEGIN

			DELETE	 dbo.MealDetail
			WHERE	 FoodTypeId = @KeyId

		END
	ELSE IF @KeyType = 'MealTypeId'
		BEGIN

			DELETE	 dbo.MealDetail
			WHERE	 MealId  IN 
					(
						SELECT		MealId 
						FROM		dbo.Meal
						WHERE		MealTypeId = @KeyId
					) 

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.MealDetail
			WHERE	 MealId  IN 
					(
						SELECT		MealId 
						FROM		dbo.Meal
						WHERE		StudentId = @KeyId
					) 

		END  	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= 'MealDetails'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
