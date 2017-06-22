IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseList')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseList'
	DROP  Procedure  dbo.SystemForeignRelationshipDatabaseList
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseList'
GO

/******************************************************************************
**		File: 
**		Name: SystemForeignRelationshipDatabaseList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SystemForeignRelationshipDatabaseList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL		
	,	@ApplicationId			INT			= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN

	SELECT	SystemForeignRelationshipDatabaseId	
		,	ApplicationId   
		,	Name		  	
		,	SortOrder
	FROM	dbo.SystemForeignRelationshipDatabase 
	WHERE	ApplicationId = ISNULL(@ApplicationId, ApplicationId)
	ORDER BY SortOrder	ASC
		 
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO