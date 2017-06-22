IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealList')
BEGIN
	PRINT 'Dropping Procedure MealList'
	DROP PROCEDURE MealList
END
GO

PRINT 'Creating Procedure MealList'
GO

/******************************************************************************
**		File: 
**		Name: MealList
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
CREATE Procedure dbo.MealList
(
		@AuditId				INT
	,	@ApplicationId			INT		    = NULL			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Meal'
)
AS
BEGIN
		SELECT	MealId
			,	ApplicationId
			,	StudentId
			,	Date
			,	MealTypeId
		FROM	dbo.Meal 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY MealId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO





		

