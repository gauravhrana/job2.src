IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealDetailList')
BEGIN
	PRINT 'Dropping Procedure MealDetailList'
	DROP PROCEDURE MealDetailList
END
GO

PRINT 'Creating Procedure MealDetailList'
GO

/******************************************************************************
**		File: 
**		Name: MealDetailList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.MealDetailList
(
		@AuditId				INT
	,	@ApplicationId			INT		    = NULL			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MealDetail'
)
AS
BEGIN
		SELECT	a.MealDetailId
			,	a.ApplicationId		
			,	a.MealId		
			,	a.FoodTypeId	
			,	a.AmtFinished
		FROM	dbo.MealDetail a
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY MealDetailId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO





		

