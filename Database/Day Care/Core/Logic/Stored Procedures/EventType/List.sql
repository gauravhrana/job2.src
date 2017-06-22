IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeList')
BEGIN
	PRINT 'Dropping Procedure EventTypeList'
	DROP PROCEDURE EventTypeList
END
GO

PRINT 'Creating Procedure EventTypeList'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeList
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
CREATE Procedure dbo.EventTypeList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'EventType'
)
AS
BEGIN
		SELECT	EventTypeId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM	dbo.EventType 
		WHERE ApplicationId 		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY EventTypeId		ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
