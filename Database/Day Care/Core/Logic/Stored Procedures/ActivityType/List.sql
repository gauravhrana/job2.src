IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeList')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeList'
	DROP PROCEDURE ActivityTypeList
END
GO

PRINT 'Creating Procedure ActivityTypeList'
GO

/******************************************************************************
**		File: 
**		Name: ActivityTypeList
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

CREATE Procedure dbo.ActivityTypeList
(
		@AuditId				INT	
	,	@ApplicationId		    INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ActivityType'
)
AS
BEGIN
		SELECT	ActivityTypeId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.ActivityType 
		WHERE   ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY ActivityTypeId		ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
