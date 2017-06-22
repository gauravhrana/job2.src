IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FoodTypeList')
BEGIN
	PRINT 'Dropping Procedure FoodTypeList'
	DROP PROCEDURE FoodTypeList
END
GO

PRINT 'Creating Procedure FoodTypeList'
GO

/******************************************************************************
**		File: 
**		Name: FoodTypeList
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
CREATE Procedure dbo.FoodTypeList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FoodType'
)
AS
BEGIN
		SELECT	FoodTypeId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.FoodType 
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY FoodTypeId			ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

