IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure MealTypeDeleteHard'
	DROP  Procedure MealTypeDeleteHard
END
GO

PRINT 'Creating Procedure MealTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: MealTypeDeleteHard
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

CREATE Procedure dbo.MealTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'MealType'
)
AS
BEGIN

	IF @KeyType = 'MealTypeId'
		BEGIN 

			EXEC	dbo.MealDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'MealTypeId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.MealType
			WHERE	MealTypeId = @KeyId

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
