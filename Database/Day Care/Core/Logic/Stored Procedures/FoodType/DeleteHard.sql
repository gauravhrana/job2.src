IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure FoodTypeDeleteHard'
	DROP  Procedure FoodTypeDeleteHard
END
GO

PRINT 'Creating Procedure FoodTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: FoodTypeDeleteHard
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

CREATE Procedure dbo.FoodTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'FoodType'
)
AS
BEGIN

	IF @KeyType = 'FoodTypeId'
		BEGIN 

			EXEC	dbo.MealDetailDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'FoodTypeId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.FoodType
			WHERE	FoodTypeId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType			= @SystemEntityType
		,	@EntityKey					= @KeyId
		,	@AuditAction				= 'DeleteHard'
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId			= @AuditId
		
END
GO
