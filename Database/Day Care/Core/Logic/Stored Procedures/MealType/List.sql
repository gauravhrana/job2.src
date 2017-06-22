IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MealTypeList')
BEGIN
	PRINT 'Dropping Procedure MealTypeList'
	DROP PROCEDURE MealTypeList
END
GO

PRINT 'Creating Procedure MealTypeList'
GO

/******************************************************************************
**		File: 
**		Name: MealTypeList
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

CREATE Procedure dbo.MealTypeList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MealType'
)
AS
BEGIN
		SELECT	MealTypeId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.MealType 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY MealTypeId			ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
