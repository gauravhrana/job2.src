IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MealDeleteHard'
	DROP  Procedure MealDeleteHard
END
GO

PRINT 'Creating Procedure MealDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: MealDelete
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

CREATE Procedure dbo.MealDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Meal'
)
AS
BEGIN

	IF @KeyType = 'MealId'
		BEGIN

			EXEC	dbo.MealDetailDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'MealId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.Meal
			WHERE	 MealId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			EXEC	dbo.MealDetailDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'StudentId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.Meal
			WHERE	 StudentId = @KeyId

		END
	ELSE IF @KeyType = 'MealTypeId'
		BEGIN

			EXEC	dbo.MealDetailDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'MealTypeId'
				,	@AuditId 	=	@AuditId

			DELETE	 dbo.Meal
			WHERE	 MealTypeId = @KeyId

		END 
	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
